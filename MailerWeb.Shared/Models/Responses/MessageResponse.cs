using System;
using System.Collections.Generic;
using MimeKit;

namespace MailerWeb.Shared.Models.Responses
{
    public class MessageResponse
    {
        public MessageResponse()
        {

        }

        public MessageResponse(MimeMessage message, bool textBody)
        {
            MessageId = message.MessageId;
            Date = message.Date;
            Attachments = message.Attachments;

            From = new List<Address>();
            To = new List<Address>();

            foreach (var fromItem in message.From)
                From.Add(new Address
                    {Name = ((MailboxAddress) fromItem).Name, Email = ((MailboxAddress) fromItem).Address});

            foreach (var toItem in message.To)
                To.Add(new Address {Name = toItem.Name, Email = ((MailboxAddress) toItem).Address});
            HtmlBody = message.HtmlBody;
            if (textBody)
                TextBody = message.TextBody;
        }

        public int Status { get; set; }
        public int Code { get; set; }
        public string MessageId { get; set; }
        public DateTimeOffset Date { get; set; }
        public IEnumerable<MimeEntity> Attachments { get; set; }
        public List<Address> From { get; set; }
        public List<Address> To { get; set; }
        public string HtmlBody { get; set; }
        public string TextBody { get; set; }
    }
}