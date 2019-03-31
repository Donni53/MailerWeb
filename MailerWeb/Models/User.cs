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
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        [Required]
        public virtual ConnectionConfiguration ConnectionSettings { get; set; }
        public virtual ICollection<Signature> Signatures { get; set; }
        public virtual Settings Settings { get; set; }

        public User()
        {
            Signatures = new List<Signature>();
        }
    }
}
