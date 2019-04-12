using System;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace MailerWeb.Models.DataManager
{
    public class ConnectionDataManager : IConnectionDataRepository<ConnectionConfiguration>
    {
        private readonly DataBaseContext _db;

        public ConnectionDataManager(DataBaseContext db)
        {
            _db = db;
        }

        public async Task<ConnectionConfiguration> GetByDomain(string domain)
        {
            return await _db.ConnectionConfigurations
                .FirstOrDefaultAsync(e => string.Equals(e.DomainsList.FirstOrDefault().Domain, domain,
                    StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<ConnectionConfiguration> GetByAddress(string imapAddress, string smtpAddress)
        {
            return await _db.ConnectionConfigurations.FirstOrDefaultAsync(e =>
                string.Equals(e.ImapConfiguration.Address, imapAddress, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(e.SmtpConfiguration.Address, smtpAddress, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task AddAsync(ConnectionConfiguration entity)
        {
            await _db.ConnectionConfigurations.AddAsync(entity);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}