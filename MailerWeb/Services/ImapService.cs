using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;

namespace MailerWeb.Services
{
    public class ImapService : IImapService
    {
        public ImapService()
        {
            Client = new ImapClient();
        }

        public ImapClient Client { get; set; }

        public void AcceptAllSslCertificates(bool value)
        {
            Client.ServerCertificateValidationCallback = (s, c, h, e) => value;
        }

        public async Task ConnectAsync(string address, int port, bool ssl)
        {
            await Client.ConnectAsync(address, port, ssl);
        }

        public async Task AuthenticateAsync(string login, string password)
        {
            await Client.AuthenticateAsync(login, password);
        }

        public async Task OpenFolder(IMailFolder folder)
        {
            if (!folder.IsOpen)
                await folder.OpenAsync(FolderAccess.ReadWrite);
        }

        public async Task UpdateFolder(IMailFolder folder)
        {
            await folder.StatusAsync(StatusItems.MailboxId);
            await folder.StatusAsync(StatusItems.Recent);
            await folder.StatusAsync(StatusItems.Unread);
            await folder.StatusAsync(StatusItems.Size);
            await folder.StatusAsync(StatusItems.Count);
        }

        public async Task<IList<IMailFolder>> GetFoldersAsync()
        {
            var folders = await Client.GetFoldersAsync(Client.PersonalNamespaces.FirstOrDefault());
            return folders;
        }

        public async Task<IMailFolder> GetMailFolderByNameAsync(string name)
        {
            var folder = await Client.GetFolderAsync(name);
            return folder;
        }

        public async Task<IMailFolder> CreateFolderAsync(string displayName)
        {
            var topLevelFolder = await Client.GetFolderAsync(Client.PersonalNamespaces.FirstOrDefault()?.Path);
            var newFolder = await topLevelFolder.CreateAsync(displayName, true);
            return newFolder;
        }

        public async Task<IMailFolder> CreateSubfolderAsync(IMailFolder folder, string displayName)
        {
            await OpenFolder(folder);
            var newFolder = await folder.CreateAsync(displayName, true);
            return newFolder;
        }

        public async Task DeleteFolderAsync(IMailFolder folder)
        {
            await folder.DeleteAsync();
        }

        public async Task<IList<IMessageSummary>> GetMessagesRangeAsync(int min, int max, IMailFolder folder,
            MessageSummaryItems items)
        {
            await OpenFolder(folder);
            var messages = await folder.FetchAsync(min, max, items);
            return messages;
        }


        public async Task MoveMessages(IList<int> indexList, IMailFolder folder, IMailFolder destFolder)
        {
            await folder.MoveToAsync(indexList, destFolder);
        }

        public async Task DeleteMessages(IList<int> indexList, IMailFolder folder)
        {
            await folder.AddFlagsAsync(indexList, MessageFlags.Deleted, true);
            await folder.ExpungeAsync();
        }

        public async Task MarkSeen(IList<int> indexList, IMailFolder folder)
        {
            await folder.AddFlagsAsync(indexList, MessageFlags.Seen, true);
        }

        public async Task MarkUnseen(IList<int> indexList, IMailFolder folder)
        {
            await folder.RemoveFlagsAsync(indexList, MessageFlags.Seen, true);
        }

        public async Task MarkFlagged(IList<int> indexList, IMailFolder folder)
        {
            await folder.AddFlagsAsync(indexList, MessageFlags.Flagged, true);
        }

        public async Task MarkUnflagged(IList<int> indexList, IMailFolder folder)
        {
            await folder.RemoveFlagsAsync(indexList, MessageFlags.Flagged, true);
        }

        public async Task MarkAnswered(IList<int> indexList, IMailFolder folder)
        {
            await folder.AddFlagsAsync(indexList, MessageFlags.Answered, true);
        }

        public async Task MarkUnanswered(IList<int> indexList, IMailFolder folder)
        {
            await folder.RemoveFlagsAsync(indexList, MessageFlags.Answered, true);
        }
    }
}