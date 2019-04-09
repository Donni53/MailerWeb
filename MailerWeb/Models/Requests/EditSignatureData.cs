using System.ComponentModel.DataAnnotations;

namespace MailerWeb.Models.Requests
{
    public class EditSignatureData
    {
        [Required] public string Token { get; set; }
        [Required] public int SignatureId { get; set; }
        [Required] public Signature NewSignature { get; set; }
    }
}