using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public DbSet<ImapConfiguration> ImapConfigurations { get; set; } 
        public DbSet<SmtpConfiguration> SmtpConfigurations { get; set; }

    }
}
