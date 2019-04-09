using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Models.Exceptions;
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

        public async Task<Signature> AddSignatureAsync(string login, Signature signature)
        {
            var user = await _db.Users.FirstOrDefaultAsync(e => e.Login == login);
            user?.Signatures.Add(signature);
            return user?.Signatures.LastOrDefault();
        }

        public async Task<Signature> GetSignatureAsync(string login, int signatureId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(e => e.Login == login);
            return user.Signatures.FirstOrDefault(e => e.Id == signatureId);
        }

        public async Task<IList<Signature>> GetSignaturesAsync(string login)
        {
            var user = await _db.Users.FirstOrDefaultAsync(e => e.Login == login);
            return user.Signatures.ToList();
        }

        public async Task DeleteSignatureAsync(string login, int signatureId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(e => e.Login == login);
            user.Signatures.Remove(user.Signatures.FirstOrDefault(e => e.Id == signatureId));
        }

        public async Task<Signature> EditSignatureAsync(string login, int signatureId, Signature newSignature)
        {
            var user = await _db.Users.FirstOrDefaultAsync(e => e.Login == login);
            var oldSignature = user.Signatures.FirstOrDefault(e => e.Id == signatureId);
            if (oldSignature == null) throw new ArgumentException("Wrong signature Id");
            oldSignature.Name = newSignature.Name;
            oldSignature.SignatureText = newSignature.SignatureText;
            return oldSignature;
        }

        public async Task EditNameAsync(string login, string name)
        {
            var user = await _db.Users.FirstOrDefaultAsync(e => e.Login == login);
            if (user != null) user.Name = name;
            else
                throw new NullUserException();
        }

        public async Task<User> GetAsync(long id)
        {
            return await _db.Users
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await _db.Users.FirstOrDefaultAsync(e => e.Login == login);
        }

        public void Update(User entity)
        {
            _db.Users.Update(entity);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}