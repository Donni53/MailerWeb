using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailerWeb.Shared.Models
{
    public class SmtpConfiguration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] public string Address { get; set; }

        [Required] public int Port { get; set; }
    }
}