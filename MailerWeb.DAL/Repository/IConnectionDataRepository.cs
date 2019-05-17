using System.Threading.Tasks;

namespace MailerWeb.DAL.Repository
{
    public interface IConnectionDataRepository<TEntity>
    {
        Task<TEntity> GetByDomain(string domain);
        Task<TEntity> GetByAddress(string imapAddress, string smtpAddress);
        Task AddAsync(TEntity entity);
        Task SaveAsync();
    }
}