using System.ComponentModel.DataAnnotations;

namespace MailerWeb.Shared.Models.Requests
{
    public class FolderData
    {
        [Required] public string Token { get; set; }

        [Required] public string FolderName { get; set; }

        [Required] public bool AllFolders { get; set; }
    }
}