using System.Threading.Tasks;
using MailerWeb.Models;
using MailerWeb.Models.Requests;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;

namespace MailerWeb.Services
{
    public interface IAuthService
    {
        Task<ConnectionConfiguration> GetConnectionConfiguration(User dbUser, SignCredentials credentials);
        Task<string> SignInAsync(SignCredentials credentials);
        Task<RefreshData> GetRefreshData(string token);
        Task<ImapClient> ImapRefresh(string token);
        Task<SmtpClient> SmtpRefresh(string token);
    }
}