using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Models.Repository;

namespace MailerWeb.Models.DataManager
{
    public class UserManager : IUserRepository<User>
    {
        private readonly DataBaseContext _db;
        public UserManager(DataBaseContext dbContext)
        {
            _db = dbContext;
        }
        public async Task Add(User entity)
        {
            await _db.Users.AddAsync(entity);
        }

        public void Delete(User entity)
        {
            _db.Users.Remove(entity);
        }

        public User Get(long id)
        {
            return _db.Users
                .FirstOrDefault(e => e.Id == id);
        }

        public User GetByLogin(string login)
        {
            return _db.Users
                .FirstOrDefault(e => e.Login == login);
        }

        public void Update(User dbEntity, User entity)
        {
            dbEntity.Login = entity.Login;
            dbEntity.Password = entity.Password;
            dbEntity.Name = entity.Name;
            dbEntity.Nickname = entity.Nickname;
            dbEntity.ImapSettings = entity.ImapSettings;
            dbEntity.SmtpSettings = entity.SmtpSettings;
            dbEntity.Signatures = entity.Signatures;
            dbEntity.Settings = entity.Settings;
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
