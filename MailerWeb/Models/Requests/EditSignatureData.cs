using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models.Requests
{
    public class EditSignatureData
    {
        [Required] public string Token { get; set; }
        [Required] public int SignatureId { get; set; }
        [Required] public Signature NewSignature { get; set; }
    }
}
