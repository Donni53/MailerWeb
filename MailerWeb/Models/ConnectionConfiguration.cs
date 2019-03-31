using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models
{
    public class ConnectionConfiguration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public virtual ImapConfiguration ImapConfiguration { get; set; }
        [Required]
        public virtual SmtpConfiguration SmtpConfiguration { get; set; }
        public virtual ICollection<EmailDomain> DomainsList { get; set; }

        public ConnectionConfiguration()
        {
            DomainsList = new List<EmailDomain>();
        }
    }
}
