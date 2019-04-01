using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;

namespace MailerWeb.Services
{
    public interface IImapMailService
    {
        ImapService ClientService { get; set; }
        Task<IList<IMailFolder>> GetFoldersAsync(string token);
    }
}
