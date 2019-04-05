using System.Threading.Tasks;
using MailerWeb.Models;
using MailerWeb.Models.Requests;

namespace MailerWeb.Services
{
    public interface IAuthService
    {
        Task<string> SignUpAsync(User user);
        Task<string> SignInAsync(SignInCredentials signInCredentials);
    }
}