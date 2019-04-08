using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MailerWeb.Models.Requests
{
    public class AddSignatureData
    {
        [Required] public string Token { get; set; }
        [Required] public Signature Signature { get; set; }
    }
}
