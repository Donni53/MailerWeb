using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MimeKit;

namespace MailerWeb.Models
{
    public class Address
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }


    public class MailEnvelope
    {
        public int Index { get; set; }
        public string MessageId { get; set; }
        public DateTimeOffset? Date { get; set; }
        public List<Address> From { get; set; }
        public List<Address> To { get; set; }
        public string Subject { get; set; }
        public bool IsSeen { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsAnswered { get; set; }
        public MessageFlags? Flags { get; set; }
    }
}
