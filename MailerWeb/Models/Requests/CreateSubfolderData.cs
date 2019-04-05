using System.ComponentModel.DataAnnotations;

namespace MailerWeb.Models.Requests
{
    public class CreateSubfolderData : CreateFolderData
    {
        [Required] public string SubfolderName { get; set; }
    }
}