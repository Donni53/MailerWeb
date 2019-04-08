using System.Collections.Generic;
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
        Task<Signature> AddSignature(string login, Signature signature);
        Task<Signature> GetSignature(string login, int signatureId);
        Task<IList<Signature>> GetSignatures(string login);
        Task DeleteSignature(string login, int signatureId);
        Task<Signature> EditSignature(string login, int signatureId, Signature newSignature);
        Task EditNames(string login, string name, string nickname);
        Task SaveAsync();
    }
}