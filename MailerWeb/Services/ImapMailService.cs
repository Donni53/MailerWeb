using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailerWeb.Models;
using MailKit;
using MailKit.Net.Imap;
using Microsoft.Extensions.Caching.Memory;
using MimeKit;
using MailFolder = MailerWeb.Models.MailFolder;

namespace MailerWeb.Services
{
    public class ImapMailService// : IImapMailService
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


        public async Task<List<MailFolder>> GetFoldersAsync(string token)
        {
            if (!_memoryCache.TryGetValue($"{token}:imap", out ImapClient client))
            {
                client = await _authService.ImapRefresh(token);
            }

            _imapService.Client = client;
            var foldersList = await _imapService.GetFoldersAsync();

            var shortFoldersList = new List<MailFolder>();

            foreach (var item in foldersList)
            {
                await _imapService.UpdateFolder(item);
                shortFoldersList.Add(new MailFolder() { Id = item.Id, Name = item.FullName, Count = item.Count, Recent = item.Recent, Unread = item.Unread, Size = item.Size});
            }

            return shortFoldersList;
        }

        public async Task<IList<MailEnvelope>> GetFolderMessagesAsync(string token, int min, int max, string folderName)
        {
            if (!_memoryCache.TryGetValue($"{token}:imap", out ImapClient client))
            {
                client = await _authService.ImapRefresh(token);
            }

            _imapService.Client = client;
            var folder = await _imapService.GetMailFolderByNameAsync(folderName);
            var mailList = await _imapService.GetMessagesRangeAsync(min, max, folder, MessageSummaryItems.All);
            var mailEnvelops = new List<MailEnvelope>();
            foreach (var item in mailList)
            {
                var envelope = new MailEnvelope()
                {
                    Index = item.Index,
                    MessageId = item.Envelope.MessageId,
                    Date = item.Envelope.Date,
                    From = new List<Address>(),
                    To = new List<Address>(),
                    Subject = item.Envelope.Subject,
                    Flags = item.Flags,
                    IsSeen = item.Flags != null && (item.Flags.Value & MessageFlags.Seen) != 0,
                    IsAnswered = item.Flags != null && (item.Flags.Value & MessageFlags.Answered) != 0,
                    IsFlagged = item.Flags != null && (item.Flags.Value & MessageFlags.Flagged) != 0,
                };
                
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
    }
}
