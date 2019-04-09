﻿using System.ComponentModel.DataAnnotations;

namespace MailerWeb.Models.Requests
{
    public class AddSignatureData
    {
        [Required] public string Token { get; set; }
        [Required] public Signature Signature { get; set; }
    }
}