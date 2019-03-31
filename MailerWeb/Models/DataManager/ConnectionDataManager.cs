using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MailerWeb.Models.Repository;

namespace MailerWeb.Models.DataManager
{
    public class ConnectionDataManager : IConnectionDataRepository<ConnectionConfiguration>
    {
        private readonly DataBaseContext _db;

        public ConnectionDataManager(DataBaseContext db)
        {
            _db = db;
        }

        public ConnectionConfiguration GetByDomain(string domain)
        {
            return _db.ConnectionConfigurations
                .FirstOrDefault(e => string.Equals(e.DomainsList.FirstOrDefault().Domain, domain, StringComparison.CurrentCultureIgnoreCase));
        }

        public ConnectionConfiguration GetByAddress(string imapAddress, string smtpAddress)
        {
            return _db.ConnectionConfigurations.FirstOrDefault(e =>
                string.Equals(e.ImapConfiguration.Address, imapAddress, StringComparison.CurrentCultureIgnoreCase) 
                && string.Equals(e.SmtpConfiguration.Address, smtpAddress, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task AddAsync(ConnectionConfiguration entity)
        {
            await _db.ConnectionConfigurations.AddAsync(entity);
        }

        public async Task SaveAsync()
        {
            await  _db.SaveChangesAsync();
        }
    }
}
