﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;

namespace MailerWeb.Server.Services
{
    public interface IImapService
    {
        ImapClient Client { get; set; }
        void AcceptAllSslCertificates(bool value);

        Task ConnectAsync(string address, int port, bool ssl);

        Task AuthenticateAsync(string login, string password);

        Task OpenFolder(IMailFolder folder);
        Task UpdateFolder(IMailFolder folder);

        Task<IList<IMailFolder>> GetFoldersAsync();
        Task<IMailFolder> GetMailFolderByNameAsync(string name);

        Task<IMailFolder> CreateFolderAsync(string displayName);

        Task<IMailFolder> CreateSubfolderAsync(IMailFolder folder, string displayName);

        Task DeleteFolderAsync(IMailFolder folder);

        Task<IList<IMessageSummary>> GetMessagesRangeAsync(int min, int max, IMailFolder folder,
            MessageSummaryItems items);

        Task MoveMessages(IList<int> indexList, IMailFolder folder, IMailFolder destFolder);

        Task DeleteMessages(IList<int> indexList, IMailFolder folder);

        Task MarkSeen(IList<int> indexList, IMailFolder folder);

        Task MarkUnseen(IList<int> indexList, IMailFolder folder);

        Task MarkFlagged(IList<int> indexList, IMailFolder folder);

        Task MarkUnflagged(IList<int> indexList, IMailFolder folder);

        Task MarkAnswered(IList<int> indexList, IMailFolder folder);

        Task MarkUnanswered(IList<int> indexList, IMailFolder folder);
    }
}