using MailerWeb.Server.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Builder;

namespace MailerWeb.Server.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}