using System.Threading.Tasks;
using MailerWeb.Models;
using MailerWeb.Models.Requests;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;

namespace MailerWeb.Services
{
    public interface IAuthService
    {
        Task<string> SignUpAsync(User user);
        Task<string> SignInAsync(SignInCredentials signInCredentials);
        Task<ImapClient> ImapRefresh(string token);
        Task<SmtpClient> SmtpRefresh(string token);
    }
}