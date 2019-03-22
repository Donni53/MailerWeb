using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models.Repository
{
    public interface IConnectionDataRepository<TEntity>
    {
        string GetImap(string domain);
        string GetSmtp(string domain);
    }
}
