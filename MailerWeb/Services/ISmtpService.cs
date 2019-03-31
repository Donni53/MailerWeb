using System;
using System.Collections.Generic;
using System.Linq;
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

        Task SendEmail(MimeMessage message, MailboxAddress sender, IList<MailboxAddress> recipients);
        Task SendEmail(MimeMessage message);
    }
}
