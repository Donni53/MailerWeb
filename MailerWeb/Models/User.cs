using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailerWeb.Models
{
    public class User
    {
        public User()
        {
            Signatures = new List<Signature>();
            EncryptedPasswords = new List<EncryptedPassword>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
        public virtual List<EncryptedPassword> EncryptedPasswords { get; set; }

        public string Name { get; set; }

        public virtual ConnectionConfiguration ConnectionSettings { get; set; }

        public virtual ICollection<Signature> Signatures { get; set; }
    }
}