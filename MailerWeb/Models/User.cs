using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public ImapConfiguration ImapSettings { get; set; }
        public SmtpConfiguration SmtpSettings { get; set; }
        public List<Signature> Signatures { get; set; }
        public Settings Settings { get; set; }

    }
}
