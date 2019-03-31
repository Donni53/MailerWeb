using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Models;
using MailerWeb.Models.Responses;
using MailerWeb.Services;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MailerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImapController : ControllerBase
    {

        private readonly AuthService _authService;

        public ImapController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp([FromBody]User user)
        {
            try
            {
                var token = await _authService.SignUpAsync(user);
                return StatusCode(200, new TokenResponse() { Status = 200, Code = 0, Token = token });
            }
            catch (Exception e)
            {
                var statusCode = 500;
                if (e is AuthenticationException)
                    statusCode = 401;

                return StatusCode(statusCode, new ErrorResponse() { Status = 500, DeveloperMessage = e.Source, UserMessage = e.Message, MoreInfo = e.HelpLink, ErrorCode = e.HResult });
            }
        }


        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn([FromBody]SignInCredentials signInCredentials)
        {
            try
            {
                var token = await _authService.SignInAsync(signInCredentials);
                return StatusCode(200, new TokenResponse() { Status = 200, Code = 0, Token = token });
            }
            catch (Exception e)
            {
                var statusCode = 500;
                if (e is AuthenticationException)
                    statusCode = 401;

                return StatusCode(statusCode, new ErrorResponse() { Status = 500, DeveloperMessage = e.Source, UserMessage = e.Message, MoreInfo = e.HelpLink, ErrorCode = e.HResult });
            }
        }

    }
}