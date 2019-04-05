using System.ComponentModel.DataAnnotations;

namespace MailerWeb.Models.Requests
{
    public class SignInCredentials
    {
        [Required] public string Login { get; set; }

        [Required] public string Password { get; set; }
    }
}