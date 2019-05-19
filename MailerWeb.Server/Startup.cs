using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System.Linq;
using MailerWeb.DAL;
using MailerWeb.DAL.DataManager;
using MailerWeb.DAL.Repository;
using MailerWeb.Server.Extensions;
using MailerWeb.Server.Services;
using MailerWeb.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

namespace MailerWeb.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();
            /*services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });*/
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
            //services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mailer API V1"); });
            app.ConfigureCustomExceptionMiddleware();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseBlazor<Client.Startup>();
        }
    }
}
