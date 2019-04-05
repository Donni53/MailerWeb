using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace MailerWeb.Services
{
    public interface ISmtpService
    {
        SmtpClient Client { get; set; }
        void AcceptAllSslCertificates(bool value);

        Task ConnectAsync(string address, int port, bool ssl);

        Task AuthenticateAsync(string login, string password);

        Task SendEmailAsync(MimeMessage message, MailboxAddress sender, IList<MailboxAddress> recipients);
        Task SendEmailAsync(MimeMessage message);
    }
}