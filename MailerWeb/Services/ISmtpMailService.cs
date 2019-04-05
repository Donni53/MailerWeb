using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Models;

namespace MailerWeb.Services
{
    public interface ISmtpMailService
    {
        Task RefreshSmtpAsync(string token);

        Task SendEmailAsync(string token, Address from, IList<Address> to, string subject, string htmlBody);
    }
}
