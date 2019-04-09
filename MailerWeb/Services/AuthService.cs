using System;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MailerWeb.Models;
using MailerWeb.Models.Exceptions;
using MailerWeb.Models.Repository;
using MailerWeb.Models.Requests;
using MailerWeb.Security;
using MailKit.Net.Imap;
using Microsoft.Extensions.Caching.Memory;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace MailerWeb.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConnectionDataRepository<ConnectionConfiguration> _connectionDataRepository;
        private readonly IUserRepository<User> _dataRepository;
        private readonly IImapService _imapService;
        private readonly IMemoryCache _memoryCache;
        private readonly ISmtpService _smtpService;

        public AuthService(
            IUserRepository<User> dataRepository, IImapService imapService, ISmtpService smtpService,
            IMemoryCache memoryCache,
            IConnectionDataRepository<ConnectionConfiguration> connectionDataRepository)
        {
            _dataRepository = dataRepository;
            _imapService = imapService;
            _smtpService = smtpService;
            _memoryCache = memoryCache;
            _connectionDataRepository = connectionDataRepository;
        }


        public async Task<string> SignInAsync(SignCredentials credentials)
        {
            var dbUser = await _dataRepository.GetByLoginAsync(credentials.Login);

            ConnectionConfiguration connectionConfiguration = null;

            if (dbUser == null)
            {
                if (credentials.ConnectionSettings == null)
                {
                    var mailAddress = new MailAddress(credentials.Login);
                    connectionConfiguration = _connectionDataRepository.GetByDomain(mailAddress.Host);
                    if (connectionConfiguration == null)
                        throw new ConnectionDataException();
                }
                else
                {
                    if (credentials.NewConnectionSettings && credentials.ConnectionSettings != null)
                    {
                        connectionConfiguration = credentials.ConnectionSettings;
                    }
                    else
                    {
                        connectionConfiguration = _connectionDataRepository.GetByAddress(
                            credentials.ConnectionSettings.ImapConfiguration.Address,
                            credentials.ConnectionSettings.SmtpConfiguration.Address);
                    }
                }
            }
            else
            {
                if (credentials.NewConnectionSettings && credentials.ConnectionSettings != null)
                {
                    connectionConfiguration = credentials.ConnectionSettings;
                }
                else
                {
                    connectionConfiguration = dbUser.ConnectionSettings;
                }
            }

            _imapService.AcceptAllSslCertificates(true);
            await _imapService.ConnectAsync(connectionConfiguration.ImapConfiguration.Address, connectionConfiguration.ImapConfiguration.Port, true);
            await _imapService.AuthenticateAsync(credentials.Login, credentials.Password);

            _smtpService.AcceptAllSslCertificates(true);
            await _smtpService.ConnectAsync(connectionConfiguration.SmtpConfiguration.Address, connectionConfiguration.SmtpConfiguration.Port, true);
            await _smtpService.AuthenticateAsync(credentials.Login, credentials.Password);

            var myRijndael = new RijndaelManaged();
            myRijndael.GenerateKey();
            myRijndael.GenerateIV();
            var encryptedPassword = Convert.ToBase64String(RijndaelManager.EncryptStringToBytes(credentials.Password, myRijndael.Key, myRijndael.IV));
            var base64Key = Convert.ToBase64String(myRijndael.Key);
            var base64Iv = Convert.ToBase64String(myRijndael.IV);
            var hashedPassword = MailerWeb.Security.Sha256.GetHashString(credentials.Password);
            var encryptedPasswordEntity = new EncryptedPassword(encryptedPassword);

            if (dbUser != null)
            {
                dbUser.ConnectionSettings = connectionConfiguration;
                dbUser.Password = hashedPassword;
                dbUser.EncryptedPasswords.Add(encryptedPasswordEntity);
                _dataRepository.Update(dbUser);
            }
            else
            {
                dbUser = new User
                {
                    Login = credentials.Login,
                    Password = hashedPassword,
                    ConnectionSettings = connectionConfiguration,
                };
                dbUser.EncryptedPasswords.Add(encryptedPasswordEntity);
                await _dataRepository.AddAsync(dbUser);
            }

            await _dataRepository.SaveAsync();

            var token = Jwt.GenerateToken(credentials.Login, base64Key, base64Iv, encryptedPasswordEntity.Id);

            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                SlidingExpiration = TimeSpan.FromSeconds(600)
            };

            _memoryCache.Set($"{token}:imap", _imapService.Client, options);
            _memoryCache.Set($"{token}:smtp", _smtpService.Client, options);

            return token;
        }

        public async Task<RefreshData> GetRefreshData(string token)
        {
            var claims = Jwt.DecodeToken(token);
            var enumerable = claims as Claim[] ?? claims.ToArray();
            var login = enumerable.FirstOrDefault(e => e.Type == "Login")?.Value;
            var key = enumerable.FirstOrDefault(e => e.Type == "Key")?.Value;
            var vector = enumerable.FirstOrDefault(e => e.Type == "IV")?.Value;
            var encryptedPasswordId = Convert.ToInt32(enumerable.FirstOrDefault(e => e.Type == "Id")?.Value);

            var dbUser = await _dataRepository.GetByLoginAsync(login);
            if (dbUser == null)
                throw new NullUserException();

            var encryptedPassword = dbUser.EncryptedPasswords.FirstOrDefault(e => e.Id == encryptedPasswordId);

            if (encryptedPassword == null)
                throw new NullUserException();

            var password = RijndaelManager.DecryptStringFromBytes(Convert.FromBase64String(encryptedPassword?.Password),
                Convert.FromBase64String(key), Convert.FromBase64String(vector));
            return new RefreshData() { User = dbUser, Password = password };
        }

        public async Task<ImapClient> ImapRefresh(string token)
        {
            var refreshData = await GetRefreshData(token);

            _imapService.AcceptAllSslCertificates(true);
            await _imapService.ConnectAsync(refreshData.User.ConnectionSettings.ImapConfiguration.Address,
                refreshData.User.ConnectionSettings.ImapConfiguration.Port, true);
            await _imapService.AuthenticateAsync(refreshData.User.Login, refreshData.Password);

            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                SlidingExpiration = TimeSpan.FromSeconds(600)
            };

            _memoryCache.Set($"{token}:imap", _imapService.Client, options);

            return _imapService.Client;
        }


        public async Task<SmtpClient> SmtpRefresh(string token)
        {
            var refreshData = await GetRefreshData(token);

            _smtpService.AcceptAllSslCertificates(true);
            await _smtpService.ConnectAsync(refreshData.User.ConnectionSettings.SmtpConfiguration.Address,
                refreshData.User.ConnectionSettings.SmtpConfiguration.Port, true);
            await _smtpService.AuthenticateAsync(refreshData.User.Login, refreshData.Password);


            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                SlidingExpiration = TimeSpan.FromSeconds(600)
            };

            _memoryCache.Set($"{token}:smtp", _smtpService.Client, options);

            return _smtpService.Client;
        }
    }
}