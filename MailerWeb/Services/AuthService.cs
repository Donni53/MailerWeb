﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;
using MailerWeb.Models;
using MailerWeb.Models.Repository;
using MailerWeb.Security;
using Microsoft.Extensions.Caching.Memory;

namespace MailerWeb.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository<User> _dataRepository;
        private readonly IConnectionDataRepository<ConnectionConfiguration> _connectionDataRepository;
        private readonly IImapService _imapService;
        private readonly ISmtpService _smtpService;
        private readonly IMemoryCache _memoryCache;
        private readonly DataBaseContext _dataBaseContext;

        public AuthService(IUserRepository<User> dataRepository, IImapService imapService, ISmtpService smtpService, IMemoryCache memoryCache, DataBaseContext dataBaseContext, IConnectionDataRepository<ConnectionConfiguration> connectionDataRepository)
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
            var connectionConfiguration = _connectionDataRepository.GetByAddress(user.ConnectionSettings.ImapConfiguration.Address, user.ConnectionSettings.SmtpConfiguration.Address) ??
                                          _connectionDataRepository.GetByDomain(mailAddress.Host);
            if (connectionConfiguration != null)
                user.ConnectionSettings = connectionConfiguration;


            _imapService.AcceptAllSslCertificates(true);
            await _imapService.ConnectAsync(user.ConnectionSettings.ImapConfiguration.Address, user.ConnectionSettings.ImapConfiguration.Port, true);
            await _imapService.AuthenticateAsync(user.Login, user.Password);

            _smtpService.AcceptAllSslCertificates(true);
            await _smtpService.ConnectAsync(user.ConnectionSettings.SmtpConfiguration.Address, user.ConnectionSettings.SmtpConfiguration.Port, true);
            await _smtpService.AuthenticateAsync(user.Login, user.Password);


            var myRijndael = new RijndaelManaged();
            myRijndael.GenerateKey();
            myRijndael.GenerateIV();
            byte[] encrypted = RijndaelManager.EncryptStringToBytes(user.Password, myRijndael.Key, myRijndael.IV);

            user.Password = Convert.ToBase64String(encrypted);


            var dbUser = _dataRepository.GetByLogin(user.Login);


            if (dbUser != null)
            {
                _dataRepository.Update(dbUser, user);
            }
            else
            {
                if (user.ConnectionSettings.DomainsList.FirstOrDefault(x => x.Domain == mailAddress.Host) == null)
                    user.ConnectionSettings.DomainsList.Add(new EmailDomain() { Domain = mailAddress.Host });
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
                    throw new Exception("Missed connection data"); //TODO replace with custom exception
                var user = new User() { Login = signInCredentials.Login, Password = signInCredentials.Password, ConnectionSettings = connectionConfiguration, Settings = new Settings() };
                var token = await SignUpAsync(user);
                return token;
            }
        }
    }
}
