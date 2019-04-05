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
        private readonly DataBaseContext _dataBaseContext;
        private readonly IUserRepository<User> _dataRepository;
        private readonly IImapService _imapService;
        private readonly IMemoryCache _memoryCache;
        private readonly ISmtpService _smtpService;

        public AuthService(IUserRepository<User> dataRepository, IImapService imapService, ISmtpService smtpService,
            IMemoryCache memoryCache, DataBaseContext dataBaseContext,
            IConnectionDataRepository<ConnectionConfiguration> connectionDataRepository)
        {
            _dataRepository = dataRepository;
            _imapService = imapService;
            _smtpService = smtpService;
            _memoryCache = memoryCache;
            _dataBaseContext = dataBaseContext;
            _connectionDataRepository = connectionDataRepository;
        }

        public async Task<string> SignUpAsync(User user)
        {
            var mailAddress = new MailAddress(user.Login);
            var connectionConfiguration = _connectionDataRepository.GetByAddress(
                                              user.ConnectionSettings.ImapConfiguration.Address,
                                              user.ConnectionSettings.SmtpConfiguration.Address) ??
                                          _connectionDataRepository.GetByDomain(mailAddress.Host);
            if (connectionConfiguration != null)
                user.ConnectionSettings = connectionConfiguration;


            _imapService.AcceptAllSslCertificates(true);
            await _imapService.ConnectAsync(user.ConnectionSettings.ImapConfiguration.Address,
                user.ConnectionSettings.ImapConfiguration.Port, true);
            await _imapService.AuthenticateAsync(user.Login, user.Password);

            _smtpService.AcceptAllSslCertificates(true);
            await _smtpService.ConnectAsync(user.ConnectionSettings.SmtpConfiguration.Address,
                user.ConnectionSettings.SmtpConfiguration.Port, true);
            await _smtpService.AuthenticateAsync(user.Login, user.Password);


            var myRijndael = new RijndaelManaged();
            myRijndael.GenerateKey();
            myRijndael.GenerateIV();
            var encrypted = RijndaelManager.EncryptStringToBytes(user.Password, myRijndael.Key, myRijndael.IV);

            user.Password = Convert.ToBase64String(encrypted);


            var dbUser = _dataRepository.GetByLogin(user.Login);


            if (dbUser != null)
            {
                _dataRepository.Update(dbUser, user);
            }
            else
            {
                if (user.ConnectionSettings.DomainsList.FirstOrDefault(x => x.Domain == mailAddress.Host) == null)
                    user.ConnectionSettings.DomainsList.Add(new EmailDomain {Domain = mailAddress.Host});
                await _dataRepository.AddAsync(user);
            }

            await _dataRepository.SaveAsync();

            var key = Convert.ToBase64String(myRijndael.Key);
            var iv = Convert.ToBase64String(myRijndael.IV);

            var token = Jwt.GenerateToken(user.Login, key, iv);


            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                SlidingExpiration = TimeSpan.FromSeconds(600)
            };

            _memoryCache.Set($"{token}:imap", _imapService.Client, options);
            _memoryCache.Set($"{token}:smtp", _smtpService.Client, options);

            return token;
        }

        public async Task<string> SignInAsync(SignInCredentials signInCredentials)
        {
            var dbUser = _dataRepository.GetByLogin(signInCredentials.Login);

            if (dbUser != null)
            {
                dbUser.Password = signInCredentials.Password;
                var token = await SignUpAsync(dbUser);
                return token;
            }
            else
            {
                var mailAddress = new MailAddress(signInCredentials.Login);
                var connectionConfiguration = _connectionDataRepository.GetByDomain(mailAddress.Host);
                if (connectionConfiguration == null)
                    throw new ConnectionDataException();
                var user = new User
                {
                    Login = signInCredentials.Login, Password = signInCredentials.Password,
                    ConnectionSettings = connectionConfiguration, Settings = new Settings()
                };
                var token = await SignUpAsync(user);
                return token;
            }
        }


        public async Task<ImapClient> ImapRefresh(string token)
        {
            var claims = Jwt.DecodeToken(token);
            var enumerable = claims as Claim[] ?? claims.ToArray();
            var login = enumerable.FirstOrDefault(e => e.Type == "Login")?.Value;
            var key = enumerable.FirstOrDefault(e => e.Type == "Key")?.Value;
            var vector = enumerable.FirstOrDefault(e => e.Type == "IV")?.Value;
            var dbUser = _dataRepository.GetByLogin(login);
            if (dbUser == null)
                throw new NullUserException();
            var password = RijndaelManager.DecryptStringFromBytes(Convert.FromBase64String(dbUser.Password),
                Convert.FromBase64String(key), Convert.FromBase64String(vector));
            _imapService.AcceptAllSslCertificates(true);
            await _imapService.ConnectAsync(dbUser.ConnectionSettings.ImapConfiguration.Address,
                dbUser.ConnectionSettings.ImapConfiguration.Port, true);
            await _imapService.AuthenticateAsync(dbUser.Login, password);


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
            var claims = Jwt.DecodeToken(token);
            var enumerable = claims as Claim[] ?? claims.ToArray();
            var login = enumerable.FirstOrDefault(e => e.Type == "Login")?.Value;
            var key = enumerable.FirstOrDefault(e => e.Type == "Key")?.Value;
            var vector = enumerable.FirstOrDefault(e => e.Type == "IV")?.Value;
            var dbUser = _dataRepository.GetByLogin(login);
            if (dbUser == null)
                throw new NullUserException();
            var password = RijndaelManager.DecryptStringFromBytes(Convert.FromBase64String(dbUser.Password),
                Convert.FromBase64String(key), Convert.FromBase64String(vector));
            _smtpService.AcceptAllSslCertificates(true);
            await _smtpService.ConnectAsync(dbUser.ConnectionSettings.SmtpConfiguration.Address,
                dbUser.ConnectionSettings.SmtpConfiguration.Port, true);
            await _smtpService.AuthenticateAsync(dbUser.Login, password);


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