using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models
{
    public class EncryptedPassword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }

        public EncryptedPassword(string password)
        {
            Password = password;
        }
    }
}
