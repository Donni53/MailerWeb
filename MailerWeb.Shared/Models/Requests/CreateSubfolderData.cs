using System.ComponentModel.DataAnnotations;

namespace MailerWeb.Shared.Models.Requests
{
    public class CreateSubfolderData : CreateFolderData
    {
        [Required] public string SubfolderName { get; set; }
    }
}