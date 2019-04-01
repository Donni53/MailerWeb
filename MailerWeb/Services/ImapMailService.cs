using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using Microsoft.Extensions.Caching.Memory;
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
            ImapClient client;
            if (!_memoryCache.TryGetValue($"{token}:imap", out client))
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


    }
}
