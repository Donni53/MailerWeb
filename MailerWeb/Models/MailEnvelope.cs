using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MimeKit;

namespace MailerWeb.Models
{
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

        public MailEnvelope(IMessageSummary messageSummary)
        {
            Index = messageSummary.Index;
            MessageId = messageSummary.Envelope.MessageId;
            Date = messageSummary.Envelope.Date;
            From = new List<Address>();
            To = new List<Address>();
            Subject = messageSummary.Envelope.Subject;
            Flags = messageSummary.Flags;
            IsSeen = messageSummary.Flags != null && (messageSummary.Flags.Value & MessageFlags.Seen) != 0;
            IsAnswered = messageSummary.Flags != null && (messageSummary.Flags.Value & MessageFlags.Answered) != 0;
            IsFlagged = messageSummary.Flags != null && (messageSummary.Flags.Value & MessageFlags.Flagged) != 0;
        }
    }
}
