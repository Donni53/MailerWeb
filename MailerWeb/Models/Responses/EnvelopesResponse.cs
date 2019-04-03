using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models.Responses
{
    public class EnvelopesResponse
    {
        public int Status { get; set; }
        public int Code { get; set; }
        public int Count { get; set; }
        public List<MailEnvelope> Envelopes { get; set; }
    }
}
