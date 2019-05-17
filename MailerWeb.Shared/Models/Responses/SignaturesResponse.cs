using System.Collections.Generic;

namespace MailerWeb.Shared.Models.Responses
{
    public class SignaturesResponse
    {
        public int Status { get; set; }
        public int Code { get; set; }
        public int Count { get; set; }
        public List<Signature> Signatures { get; set; }
    }
}