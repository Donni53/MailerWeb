using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailerWeb.Shared.Models
{
    public class ConnectionConfiguration
    {
        public ConnectionConfiguration()
        {
            DomainsList = new List<EmailDomain>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] public virtual ImapConfiguration ImapConfiguration { get; set; }

        [Required] public virtual SmtpConfiguration SmtpConfiguration { get; set; }

        public virtual ICollection<EmailDomain> DomainsList { get; set; }
    }
}