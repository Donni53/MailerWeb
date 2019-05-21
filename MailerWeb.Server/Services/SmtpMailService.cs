using System.Collections.Generic;
using System.Threading.Tasks;
using MailerWeb.Shared.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace MailerWeb.Server.Services
{
    public class SmtpMailService : ISmtpMailService
    {
        private readonly IAuthService _authService;
        private readonly IMemoryCacheDataService _memoryCache;
        private readonly ISmtpService _smtpService;

        public SmtpMailService(ISmtpService smtpService, IMemoryCacheDataService memoryCache, IAuthService authService)
        {
            _smtpService = smtpService;
            _memoryCache = memoryCache;
            _authService = authService;
        }


        public async Task RefreshSmtpAsync(string token)
        {
            if (!_memoryCache.TryGetValue($"{token}:smtp", out var client))
                client = await _authService.SmtpRefresh(token);

            _smtpService.Client = (SmtpClient) client;
        }

        public async Task SendEmailAsync(string token, Address from, IList<Address> to, string subject, string htmlBody)
        {
            await RefreshSmtpAsync(token);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from.Name, from.Email));
            foreach (var item in to) message.To.Add(new MailboxAddress(item.Name, item.Email));
            message.Subject = subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = htmlBody
            };

            await _smtpService.SendEmailAsync(message);
        }
    }
}