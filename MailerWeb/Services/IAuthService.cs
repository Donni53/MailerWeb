using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Models;

namespace MailerWeb.Services
{
    public interface IAuthService
    {
        Task<string> SignUpAsync(User user);
        Task<string> SignInAsync(SignInCredentials signInCredentials);
    }
}
