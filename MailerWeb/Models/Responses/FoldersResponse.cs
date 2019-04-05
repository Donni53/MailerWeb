using System.Collections.Generic;

namespace MailerWeb.Models.Responses
{
    public class FoldersResponse
    {
        public int Status { get; set; }
        public int Code { get; set; }
        public int Count { get; set; }
        public List<MailFolder> Folders { get; set; }
    }
}