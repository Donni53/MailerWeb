using MailerWeb.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MailerWeb.DAL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ConnectionConfiguration> ConnectionConfigurations { get; set; }
    }
}