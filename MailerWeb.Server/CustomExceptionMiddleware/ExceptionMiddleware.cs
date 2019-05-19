using System;
using System.Threading.Tasks;
using MailerWeb.Shared.Models.Exceptions;
using MailerWeb.Shared.Models.Responses;
using MailKit;
using MailKit.Security;
using Microsoft.AspNetCore.Http;

namespace MailerWeb.Server.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            var statusCode = 500;
            switch (e)
            {
                case AuthenticationException _:
                case NullUserException _:
                case ServiceNotConnectedException _:
                case ServiceNotAuthenticatedException _:
                    statusCode = 401;
                    break;
                case ConnectionDataException _:
                case InvalidFlagException _:
                case FolderNotFoundException _:
                case MessageNotFoundException _:
                case ArgumentException _:
                case IndexOutOfRangeException _:
                    statusCode = 400;
                    break;
            }

            switch (e)
            {
                case ArgumentNullException _:
                case ArgumentOutOfRangeException _:
                    statusCode = 400;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(
                new ErrorResponse
                {
                    Status = statusCode,
                    DeveloperMessage = $"{e.Source} {nameof(e)}",
                    UserMessage = e.Message,
                    MoreInfo = e.HelpLink,
                    ErrorCode = e.HResult
                }.ToString()
            );
        }
    }
}