using System.Threading.Tasks;

namespace MailerWeb.Models.Repository
{
    public interface IUserRepository<TEntity>
    {
        TEntity Get(long id);
        TEntity GetByLogin(string login);
        Task AddAsync(TEntity entity);
        void Update(User entity, User newEntity);
        void Delete(TEntity entity);
        //TEntity AddSignature(int id, Signature signature);
        //TEntity GetSignature(int id, int signatureId);
        Task SaveAsync();
    }
}