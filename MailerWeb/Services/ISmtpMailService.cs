using System.Collections.Generic;
using System.Threading.Tasks;
using MailerWeb.Shared.Models;

namespace MailerWeb.Server.Services
{
    public interface ISmtpMailService
    {
        Task RefreshSmtpAsync(string token);

        Task SendEmailAsync(string token, Address from, IList<Address> to, string subject, string htmlBody);
    }
}