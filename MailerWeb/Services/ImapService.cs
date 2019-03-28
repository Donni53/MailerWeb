using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;

namespace MailerWeb.Services
{
    public class ImapService : IImapService
    {
        private ImapClient _client;

        public ImapService()
        {
            _client = new ImapClient();
        }

        public ImapClient Client
        {
            get => _client;
            set => _client = value;
        }

        public void AcceptAllSslCertificates(bool value)
        {
            _client.ServerCertificateValidationCallback = (s, c, h, e) => value;
        }

        public async Task ConnectAsync(string address, int port, bool ssl)
        {
            await _client.ConnectAsync(address, port, ssl);
        }

        public async Task AuthenticateAsync(string login, string password)
        {
            await _client.AuthenticateAsync(login, password);
        }

        public async Task OpenFolder(IMailFolder folder)
        {
            if (!folder.IsOpen)
                await folder.OpenAsync(FolderAccess.ReadWrite);
        }

        public async Task<IList<IMailFolder>> GetFoldersAsync()
        {
            var folders = await _client.GetFoldersAsync(_client.PersonalNamespaces.FirstOrDefault());
            return folders;
        }

        public async Task<IMailFolder> CreateFolderAsync(string displayName)
        {
            var topLevelFolder = await _client.GetFolderAsync(_client.PersonalNamespaces.FirstOrDefault()?.Path);
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

        public async Task<IList<IMessageSummary>> GetMessagesRangeAsync(int min, int max, IMailFolder folder, MessageSummaryItems items)
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
