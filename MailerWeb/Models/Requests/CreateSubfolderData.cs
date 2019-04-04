using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models.Requests
{
    public class CreateSubfolderData : CreateFolderData
    {
        [Required]
        public string SubfolderName { get; set; }
    }
}
