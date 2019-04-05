using MailerWeb.Extensions;
using MailerWeb.Models;
using MailerWeb.Models.DataManager;
using MailerWeb.Models.Repository;
using MailerWeb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailerWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataBaseContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(connection));
            services.AddScoped<IUserRepository<User>, UserManager>();
            services.AddScoped<IConnectionDataRepository<ConnectionConfiguration>, ConnectionDataManager>();
            services.AddScoped<IImapService, ImapService>();
            services.AddScoped<ISmtpService, SmtpService>();
            services.AddScoped<IImapMailService, ImapMailService>();
            services.AddScoped<ISmtpMailService, SmtpMailService>();
            services.AddScoped<AuthService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.ConfigureCustomExceptionMiddleware();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}