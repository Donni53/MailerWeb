using System.Collections.Generic;

namespace MailerWeb.Models.Requests
{
    public class MoveData
    {
        public string Token { get; set; }
        public IList<int> IndexList { get; set; }
        public string FolderName { get; set; }
        public string DestFolderName { get; set; }
    }
}