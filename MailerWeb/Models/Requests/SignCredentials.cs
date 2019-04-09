using System.ComponentModel.DataAnnotations;

namespace MailerWeb.Models.Requests
{
    public class SignCredentials
    {
        [Required] public string Login { get; set; }

        [Required] public string Password { get; set; }
        public bool NewConnectionSettings { get; set; }
        public ConnectionConfiguration ConnectionSettings { get; set; }
    }
}