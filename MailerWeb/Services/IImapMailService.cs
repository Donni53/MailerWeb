using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Models;
using MimeKit;
using MailFolder = MailKit.MailFolder;

namespace MailerWeb.Services
{
    public interface IImapMailService
    {
        Task RefreshImapAsync(string token);
        Task<List<Models.MailFolder>> GetFoldersAsync(string token);
        Task<IList<MailEnvelope>> GetFolderMessagesAsync(string token, int min, int max, string folderName);
        Task<MimeMessage> GetMessageBodyAsync(string token, string folderName, int messageIndex);
        Task<List<Models.MailFolder>> CreateFolderAsync(string token, string displayName, bool allFolders);
        Task<List<Models.MailFolder>> CreateSubfolderAsync(string token, string folderName, string displayName, bool allFolders);
        Task<List<Models.MailFolder>> DeleteFolderAsync(string token, string folderName, bool allFolders);
        Task MoveMessagesAsync(string token, IList<int> indexList, string folderName, string destFolderName);
        Task MarkMessages(string token, IList<int> indexList, string folderName, string flag);
        Task DeleteMessagesAsync(string token, IList<int> indexList, string folderName);

    }
}
