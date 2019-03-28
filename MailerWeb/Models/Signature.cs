using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models
{
    public class Signature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Max length is 100")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Signature text is required")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Max length is 1000")]
        public string SignatureText { get; set; }
    }
}
