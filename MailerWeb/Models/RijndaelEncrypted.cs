using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models
{
    public class RijndaelEncrypted
    {
        public string Data { get; set; }
        public string Key { get; set; }
        public string Iv { get; set; }
    }
}
