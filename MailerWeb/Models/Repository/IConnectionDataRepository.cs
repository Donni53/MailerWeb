using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models.Repository
{
    public interface IConnectionDataRepository<TEntity>
    {
        TEntity GetByDomain(string domain);
        TEntity GetByAddress(string imapAddress, string smtpAddress);
        Task AddAsync(TEntity entity);
        Task SaveAsync();
    }
}
