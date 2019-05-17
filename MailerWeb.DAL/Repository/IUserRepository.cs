using System.Collections.Generic;
using System.Threading.Tasks;
using MailerWeb.Shared.Models;

namespace MailerWeb.DAL.Repository
{
    public interface IUserRepository<TEntity>
    {
        Task<TEntity> GetAsync(long id);
        Task<TEntity> GetByLoginAsync(string login);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<Signature> AddSignatureAsync(string login, Signature signature);
        Task<Signature> GetSignatureAsync(string login, int signatureId);
        Task<IList<Signature>> GetSignaturesAsync(string login);
        Task DeleteSignatureAsync(string login, int signatureId);
        Task<Signature> EditSignatureAsync(string login, int signatureId, Signature newSignature);
        Task EditNameAsync(string login, string name);
        Task SaveAsync();
    }
}