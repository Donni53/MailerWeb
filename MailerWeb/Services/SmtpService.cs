using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace MailerWeb.Services
{
    public class SmtpService : ISmtpService
    {
        private SmtpClient _client;

        public SmtpService()
        {
            _client = new SmtpClient();
        }

        public SmtpClient Client
        {
            get => _client;
            set => _client = value;
        }

        public void AcceptAllSslCertificates(bool value)
        {
            _client.ServerCertificateValidationCallback = (s, c, h, e) => value;
        }

        public async Task ConnectAsync(string address, int port, bool ssl)
        {
            await _client.ConnectAsync(address, port, ssl);
        }

        public async Task AuthenticateAsync(string login, string password)
        {
            await _client.AuthenticateAsync(login, password);
        }

        public async Task SendEmailAsync(MimeMessage message, MailboxAddress sender, IList<MailboxAddress> recipients)
        {
            await _client.SendAsync(message, sender, recipients);
        }

        public async Task SendEmailAsync(MimeMessage message)
        {
            await _client.SendAsync(message);
        }
    }
}
