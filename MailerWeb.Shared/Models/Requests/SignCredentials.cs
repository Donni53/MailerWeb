using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MailerWeb.Shared.Models.Requests
{
    public class SignCredentials
    {
        [Required] public string Login { get; set; }

        [Required] public string Password { get; set; }
        public bool NewConnectionSettings { get; set; }
        public ConnectionConfiguration ConnectionSettings { get; set; }

    }
}