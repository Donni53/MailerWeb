using MailerWeb.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Builder;

namespace MailerWeb.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}