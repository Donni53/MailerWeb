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

        public async Task AddAsync(User entity)
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
            return _db
                .Users //.Include("ConnectionSettings").Include("Settings").Include("ImapConfiguration").Include("SmtpConfiguration")
                .FirstOrDefault(e => e.Login == login);
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