using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MailerWeb.Models.Requests
{
    public class MarkData
    {
        [Required] public string Token { get; set; }
        [Required] public IList<int> IndexList { get; set; }
        [Required] public string FolderName { get; set; }
        [Required] public string Flag { get; set; }
    }
}