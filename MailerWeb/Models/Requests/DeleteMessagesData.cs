using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models.Requests
{
    public class DeleteMessagesData
    {
        public string Token { get; set; }
        public IList<int> IndexList { get; set; }
        public string FolderName { get; set; }
    }
}
