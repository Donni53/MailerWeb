using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models.Requests
{
    public class FolderData
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string FolderName { get; set; }
        [Required]
        public bool AllFolders { get; set; }
    }
}
