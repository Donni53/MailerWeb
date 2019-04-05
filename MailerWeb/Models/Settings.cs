using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailerWeb.Models
{
    public class Settings
    {
        public Settings()
        {
            Theme = "Yeti";
            Localization = "ru";
            Notifications = false;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Theme { get; set; }
        public string Localization { get; set; }
        public bool Notifications { get; set; }
    }
}