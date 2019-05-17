using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace MailerWeb.Server.Services
{
    public class SmtpService : ISmtpService
    {
        public SmtpService()
        {
            Client = new SmtpClient();
        }

        public SmtpClient Client { get; set; }

        public void AcceptAllSslCertificates(bool value)
        {
            Client.ServerCertificateValidationCallback = (s, c, h, e) => value;
        }

        public async Task ConnectAsync(string address, int port, bool ssl)
        {
            await Client.ConnectAsync(address, port, ssl);
        }

        public async Task AuthenticateAsync(string login, string password)
        {
            await Client.AuthenticateAsync(login, password);
        }

        public async Task SendEmailAsync(MimeMessage message, MailboxAddress sender, IList<MailboxAddress> recipients)
        {
            await Client.SendAsync(message, sender, recipients);
        }

        public async Task SendEmailAsync(MimeMessage message)
        {
            await Client.SendAsync(message);
        }
    }
}