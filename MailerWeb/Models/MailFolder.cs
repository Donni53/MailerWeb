using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models
{
    public class MailFolder
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Recent { get; set; }
        public int Unread { get; set; }
        public ulong? Size { get; set; }
    }
}
