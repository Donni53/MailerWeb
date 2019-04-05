using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailerWeb.Models;
using MailerWeb.Models.Exceptions;
using MailKit;
using MailKit.Net.Imap;
using Microsoft.Extensions.Caching.Memory;
using MimeKit;
using MailFolder = MailerWeb.Models.MailFolder;

namespace MailerWeb.Services
{
    public class ImapMailService : IImapMailService
    {
        private readonly IImapService _imapService;
        private readonly IMemoryCache _memoryCache;
        private readonly AuthService _authService;

        public ImapMailService(IMemoryCache memoryCache, IImapService imapService, AuthService authService)
        {
            _memoryCache = memoryCache;
            _imapService = imapService;
            _authService = authService;
        }

        public async Task RefreshImapAsync(string token)
        {
            if (!_memoryCache.TryGetValue($"{token}:imap", out ImapClient client))
            {
                client = await _authService.ImapRefresh(token);
            }

            _imapService.Client = client;
        }


        public async Task<List<MailFolder>> GetFoldersAsync(string token)
        {
            await RefreshImapAsync(token);

            var foldersList = await _imapService.GetFoldersAsync();

            var shortFoldersList = new List<MailFolder>();

            foreach (var item in foldersList)
            {
                await _imapService.UpdateFolder(item);
                shortFoldersList.Add(new MailFolder(item));
            }

            return shortFoldersList;
        }

        public async Task<IList<MailEnvelope>> GetFolderMessagesAsync(string token, int min, int max, string folderName)
        {
            await RefreshImapAsync(token);
            var folder = await _imapService.GetMailFolderByNameAsync(folderName);
            var mailList = await _imapService.GetMessagesRangeAsync(min, max, folder, MessageSummaryItems.All);
            var mailEnvelops = new List<MailEnvelope>();
            foreach (var item in mailList)
            {
                var envelope = new MailEnvelope(item);
                
                foreach (var fromItem in item.Envelope.From)
                {
                    envelope.From.Add(new Address() { Name = ((MailboxAddress)fromItem).Name, Email = ((MailboxAddress)fromItem).Address });
                }

                foreach (var toItem in item.Envelope.To)
                {
                    envelope.To.Add(new Address() { Name = toItem.Name, Email = ((MailboxAddress)toItem).Address});
                }

                mailEnvelops.Add(envelope);
            }
            return mailEnvelops;
        }

        public async Task<MimeMessage> GetMessageBodyAsync(string token, string folderName, int messageIndex)
        {
            await RefreshImapAsync(token);
            var folder = await _imapService.GetMailFolderByNameAsync(folderName);
            await _imapService.OpenFolder(folder);
            var message = await folder.GetMessageAsync(index: messageIndex);
            return message;
        }

        public async Task<List<MailFolder>> CreateFolderAsync(string token, string displayName, bool allFolders)
        {
            await RefreshImapAsync(token);
            var newFolder = await _imapService.CreateFolderAsync(displayName);

            IList<IMailFolder> foldersList;

            if (allFolders)
                foldersList = await _imapService.GetFoldersAsync();
            else
                foldersList = new List<IMailFolder>() { newFolder };

            var shortFoldersList = new List<MailFolder>();

            foreach (var item in foldersList)
            {
                await _imapService.UpdateFolder(item);
                shortFoldersList.Add(new MailFolder(item));
            }

            return shortFoldersList;
        }

        public async Task<List<MailFolder>> CreateSubfolderAsync(string token, string folderName, string displayName, bool allFolders)
        {
            await RefreshImapAsync(token);
            var folder = await _imapService.GetMailFolderByNameAsync(folderName);
            var newFolder = await _imapService.CreateSubfolderAsync(folder, displayName);
            IList<IMailFolder> foldersList;

            if (allFolders)
                foldersList = await _imapService.GetFoldersAsync();
            else
                foldersList = new List<IMailFolder>() { newFolder };

            var shortFoldersList = new List<MailFolder>();

            foreach (var item in foldersList)
            {
                await _imapService.UpdateFolder(item);
                shortFoldersList.Add(new MailFolder(item));
            }

            return shortFoldersList;
        }

        public async Task<List<MailFolder>> DeleteFolderAsync(string token, string folderName, bool allFolders)
        {
            await RefreshImapAsync(token);
            var folder = await _imapService.GetMailFolderByNameAsync(folderName);
            await _imapService.DeleteFolderAsync(folder);

            IList<IMailFolder> foldersList;

            if (allFolders)
                foldersList = await _imapService.GetFoldersAsync();
            else
                foldersList = new List<IMailFolder>();

            var shortFoldersList = new List<MailFolder>();

            foreach (var item in foldersList)
            {
                await _imapService.UpdateFolder(item);
                shortFoldersList.Add(new MailFolder(item));
            }
            return shortFoldersList;
        }

        public async Task MoveMessagesAsync(string token, IList<int> indexList, string folderName, string destFolderName)
        {
            await RefreshImapAsync(token);
            var folder = await _imapService.GetMailFolderByNameAsync(folderName);
            var destFolder = await _imapService.GetMailFolderByNameAsync(destFolderName);
            await _imapService.MoveMessages(indexList, folder, destFolder);
        }

        public async Task MarkMessages(string token, IList<int> indexList, string folderName, string flag)
        {
            await RefreshImapAsync(token);
            var folder = await _imapService.GetMailFolderByNameAsync(folderName);
            switch (flag.ToLower())
            {
                case "seen": await _imapService.MarkSeen(indexList, folder);
                    break;
                case "unseen": await _imapService.MarkUnseen(indexList, folder);
                    break;
                case "flagged": await _imapService.MarkFlagged(indexList, folder);
                    break;
                case "unflagged": await _imapService.MarkUnflagged(indexList, folder);
                    break;
                case "answered": await _imapService.MarkAnswered(indexList, folder);
                    break;
                case "unanswered": await _imapService.MarkUnanswered(indexList, folder);
                    break;
                default: throw new InvalidFlagException();
            }
        }

        public async Task DeleteMessagesAsync(string token, IList<int> indexList, string folderName)
        {
            await RefreshImapAsync(token);
            var folder = await _imapService.GetMailFolderByNameAsync(folderName);
            await folder.OpenAsync(FolderAccess.ReadWrite);
            await _imapService.DeleteMessages(indexList, folder);
        }
    }
}
