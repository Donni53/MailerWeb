using System;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using MailerWeb.DAL.Repository;
using MailerWeb.Server.Security;
using MailerWeb.Shared.Models;
using MailerWeb.Shared.Models.Exceptions;
using MailerWeb.Shared.Models.Requests;
using MailKit.Net.Imap;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace MailerWeb.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConnectionDataRepository<ConnectionConfiguration> _connectionDataRepository;
        private readonly IUserRepository<User> _dataRepository;
        private readonly IImapService _imapService;
        private readonly IMemoryCacheDataService _memoryCache;
        private readonly ISmtpService _smtpService;

        public AuthService(
            IUserRepository<User> dataRepository, IImapService imapService, ISmtpService smtpService,
            IConnectionDataRepository<ConnectionConfiguration> connectionDataRepository,
            IMemoryCacheDataService memoryCache)
        {
            _dataRepository = dataRepository;
            _imapService = imapService;
            _smtpService = smtpService;
            _connectionDataRepository = connectionDataRepository;
            _memoryCache = memoryCache;
        }


        public async Task<ConnectionConfiguration> GetConnectionConfiguration(User dbUser, SignCredentials credentials)
        {
            if (dbUser == null)
            {
                if (credentials.ConnectionSettings == null)
                {
                    var mailAddress = new MailAddress(credentials.Login);
                    return await _connectionDataRepository.GetByDomain(mailAddress.Host);
                }

                if (credentials.NewConnectionSettings)
                    return credentials.ConnectionSettings;
                return await _connectionDataRepository.GetByAddress(
                    credentials.ConnectionSettings.ImapConfiguration.Address,
                    credentials.ConnectionSettings.SmtpConfiguration.Address);
            }

            if (credentials.NewConnectionSettings && credentials.ConnectionSettings != null)
                return credentials.ConnectionSettings;
            return dbUser.ConnectionSettings;
        }


        public async Task<string> SignInAsync(SignCredentials credentials)
        {
            var dbUser = await _dataRepository.GetByLoginAsync(credentials.Login);

            var connectionConfiguration = await GetConnectionConfiguration(dbUser, credentials);

            if (connectionConfiguration == null)
                throw new ConnectionDataException();

            _imapService.AcceptAllSslCertificates(true);
            await _imapService.ConnectAsync(connectionConfiguration.ImapConfiguration.Address,
                connectionConfiguration.ImapConfiguration.Port, true);
            await _imapService.AuthenticateAsync(credentials.Login, credentials.Password);

            _smtpService.AcceptAllSslCertificates(true);
            await _smtpService.ConnectAsync(connectionConfiguration.SmtpConfiguration.Address,
                connectionConfiguration.SmtpConfiguration.Port, true);
            await _smtpService.AuthenticateAsync(credentials.Login, credentials.Password);

            var encryptedPasswordData = RijndaelManager.EncryptStringToBase64String(credentials.Password);
            var hashedPassword = Sha256.GetHashString(credentials.Password);
            var encryptedPasswordEntity = new EncryptedPassword(encryptedPasswordData.Data);

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
                    ConnectionSettings = connectionConfiguration
                };
                dbUser.EncryptedPasswords.Add(encryptedPasswordEntity);
                await _dataRepository.AddAsync(dbUser);
            }

            await _dataRepository.SaveAsync();

            var token = Jwt.GenerateToken(credentials.Login, encryptedPasswordData.Key, encryptedPasswordData.Iv,
                encryptedPasswordEntity.Id);

            _memoryCache.Set($"{token}:imap", _imapService.Client);
            _memoryCache.Set($"{token}:smtp", _smtpService.Client);

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
            return new RefreshData {User = dbUser, Password = password};
        }

        public async Task<ImapClient> ImapRefresh(string token)
        {
            var refreshData = await GetRefreshData(token);

            _imapService.AcceptAllSslCertificates(true);
            await _imapService.ConnectAsync(refreshData.User.ConnectionSettings.ImapConfiguration.Address,
                refreshData.User.ConnectionSettings.ImapConfiguration.Port, true);
            await _imapService.AuthenticateAsync(refreshData.User.Login, refreshData.Password);

            _memoryCache.Set($"{token}:imap", _imapService.Client);

            return _imapService.Client;
        }


        public async Task<SmtpClient> SmtpRefresh(string token)
        {
            var refreshData = await GetRefreshData(token);

            _smtpService.AcceptAllSslCertificates(true);
            await _smtpService.ConnectAsync(refreshData.User.ConnectionSettings.SmtpConfiguration.Address,
                refreshData.User.ConnectionSettings.SmtpConfiguration.Port, true);
            await _smtpService.AuthenticateAsync(refreshData.User.Login, refreshData.Password);

            _memoryCache.Set($"{token}:smtp", _smtpService.Client);

            return _smtpService.Client;
        }
    }
}