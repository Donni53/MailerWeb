using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models.Requests
{
    public class EditNamesData
    {
        [Required] public string Token { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Nickname { get; set; }
    }
}
