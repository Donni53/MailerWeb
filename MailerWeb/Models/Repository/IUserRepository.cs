using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models.Repository
{
    public interface IUserRepository<TEntity>
    {
        TEntity Get(long id);
        TEntity GetByLogin(string login);
        Task Add(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);
        Task Save();
    }
}
