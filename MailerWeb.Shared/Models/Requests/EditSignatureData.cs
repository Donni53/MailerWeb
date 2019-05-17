using System.ComponentModel.DataAnnotations;

namespace MailerWeb.Shared.Models.Requests
{
    public class EditSignatureData
    {
        [Required] public string Token { get; set; }
        [Required] public int SignatureId { get; set; }
        [Required] public Signature NewSignature { get; set; }
    }
}