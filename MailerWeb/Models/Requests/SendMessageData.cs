using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models.Requests
{
    public class SendMessageData
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public Address From { get; set; }
        [Required]
        public IList<Address> To { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string HtmlBody { get; set; }
    }
}
