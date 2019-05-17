using System;
using MailerWeb.DAL;
using MailerWeb.DAL.DataManager;
using MailerWeb.DAL.Repository;
using MailerWeb.Server.Extensions;
using MailerWeb.Server.Services;
using MailerWeb.Shared.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MailerWeb.Server
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mailer API",
                    Version = "v1",
                    Description = "Imap and smtp web API app using Mailkit",
                    Contact = new OpenApiContact
                    {
                        Name = "Daniil Novitskiy",
                        Email = "donnipc@outlook.com",
                        Url = new Uri("https://github.com/Donni53")
                    }
                });
            });

            services.AddScoped<IUserRepository<User>, UserManager>();
            services.AddScoped<IConnectionDataRepository<ConnectionConfiguration>, ConnectionDataManager>();
            services.AddScoped<IImapService, ImapService>();
            services.AddScoped<ISmtpService, SmtpService>();
            services.AddScoped<IImapMailService, ImapMailService>();
            services.AddScoped<ISmtpMailService, SmtpMailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMemoryCacheDataService, MemoryCacheDataService>();
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
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mailer API V1"); });
            app.ConfigureCustomExceptionMiddleware();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}