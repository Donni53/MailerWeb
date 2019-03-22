using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Models.Repository;

namespace MailerWeb.Models.DataManager
{
    public class ConnectionDataManager : IConnectionDataRepository<ImapConfiguration>
    {
        private readonly DataBaseContext _db;
        public ConnectionDataManager(DataBaseContext dbContext)
        {
            _db = dbContext;
        }
        public string GetImap(string domain)
        {
            return _db.ImapConfigurations
                .FirstOrDefault(e => e.Address.Contains(domain))
                ?.Address;
        }

        public string GetSmtp(string domain)
        {
            return _db.SmtpConfigurations
                .FirstOrDefault(e => e.Address.Contains(domain))
                ?.Address;
        }
    }
}
