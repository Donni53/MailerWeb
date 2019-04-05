using System.Collections.Generic;
using System.Threading.Tasks;
using MailerWeb.Models;
using MimeKit;

namespace MailerWeb.Services
{
    public interface IImapMailService
    {
        Task RefreshImapAsync(string token);
        Task<List<MailFolder>> GetFoldersAsync(string token);
        Task<IList<MailEnvelope>> GetFolderMessagesAsync(string token, int min, int max, string folderName);
        Task<MimeMessage> GetMessageBodyAsync(string token, string folderName, int messageIndex);
        Task<List<MailFolder>> CreateFolderAsync(string token, string displayName, bool allFolders);

        Task<List<MailFolder>> CreateSubfolderAsync(string token, string folderName, string displayName,
            bool allFolders);

        Task<List<MailFolder>> DeleteFolderAsync(string token, string folderName, bool allFolders);
        Task MoveMessagesAsync(string token, IList<int> indexList, string folderName, string destFolderName);
        Task MarkMessages(string token, IList<int> indexList, string folderName, string flag);
        Task DeleteMessagesAsync(string token, IList<int> indexList, string folderName);
    }
}