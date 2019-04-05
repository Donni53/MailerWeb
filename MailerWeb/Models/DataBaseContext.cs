using Microsoft.EntityFrameworkCore;

namespace MailerWeb.Models
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