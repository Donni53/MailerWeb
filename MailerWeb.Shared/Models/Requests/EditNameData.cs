﻿using System.ComponentModel.DataAnnotations;

namespace MailerWeb.Shared.Models.Requests
{
    public class EditNameData
    {
        [Required] public string Token { get; set; }
        [Required] public string Name { get; set; }
    }
}