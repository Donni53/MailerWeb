﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MailerWeb.Shared.Models.Requests
{
    public class DeleteMessagesData
    {
        [Required] public string Token { get; set; }
        [Required] public IList<int> IndexList { get; set; }
        [Required] public string FolderName { get; set; }
    }
}