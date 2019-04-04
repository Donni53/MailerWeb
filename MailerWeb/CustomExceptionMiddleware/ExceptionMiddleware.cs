using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MailerWeb.Models;
using MailerWeb.Models.Responses;
using MailKit.Security;
using Microsoft.AspNetCore.Http;

namespace MailerWeb.CustomExceptionMiddleware
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
            if (e is AuthenticationException)
                statusCode = 401;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(
                new ErrorResponse()
                {
                    Status = statusCode,
                    DeveloperMessage = e.Source,
                    UserMessage = e.Message,
                    MoreInfo = e.HelpLink,
                    ErrorCode = e.HResult
                }.ToString()
                );
        }

    }
}
