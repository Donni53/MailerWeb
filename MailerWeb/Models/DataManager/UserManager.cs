using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace MailerWeb.Models.DataManager
{
    public class UserManager : IUserRepository<User>
    {
        private readonly DataBaseContext _db;

        public UserManager(DataBaseContext dbContext)
        {
            _db = dbContext;
        }

        public async Task AddAsync(User entity)
        {
            await _db.Users.AddAsync(entity);
        }

        public void Delete(User entity)
        {
            _db.Users.Remove(entity);
        }

        public async Task<Signature> AddSignature(string login, Signature signature)
        {
            var user = _db.Users.FirstOrDefault(e => e.Login == login);
            user?.Signatures.Add(signature);
            return user.Signatures.LastOrDefault();
        }

        public async Task<Signature> GetSignature(string login, int signatureId)
        {
            var user = _db.Users.FirstOrDefault(e => e.Login == login);
            return user.Signatures.FirstOrDefault(e => e.Id == signatureId);
        }

        public async Task<IList<Signature>> GetSignatures(string login)
        {
            var user = _db.Users.FirstOrDefault(e => e.Login == login);
            return user.Signatures.ToList();
        }

        public async Task DeleteSignature(string login, int signatureId)
        {
            var user = _db.Users.FirstOrDefault(e => e.Login == login);
            user.Signatures.Remove(user.Signatures.FirstOrDefault(e => e.Id == signatureId));
        }

        public async Task<Signature> EditSignature(string login, int signatureId, Signature newSignature)
        {
            var user = _db.Users.FirstOrDefault(e => e.Login == login);
            var oldSignature = user.Signatures.FirstOrDefault(e => e.Id == signatureId);
            oldSignature.Name = newSignature.Name;
            oldSignature.SignatureText = newSignature.SignatureText;
            return oldSignature;
        }

        public async Task EditNames(string login, string name, string nickname)
        {
            var user = _db.Users.FirstOrDefault(e => e.Login == login);
            user.Name = name;
            user.Nickname = nickname;
        }

        public User Get(long id)
        {
            return _db.Users
                .FirstOrDefault(e => e.Id == id);
        }

        public User GetByLogin(string login)
        {
            return _db.Users.FirstOrDefault(e => e.Login == login);
        }

        public void Update(User entity, User newEntity)
        {
            entity.Password = newEntity.Password;
            entity.Name = newEntity.Name;
            entity.Nickname = newEntity.Nickname;
            entity.ConnectionSettings = newEntity.ConnectionSettings;

            _db.Users.Update(entity);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}