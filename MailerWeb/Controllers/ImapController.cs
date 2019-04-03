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
using Newtonsoft.Json;

namespace MailerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImapController : ControllerBase
    {

        private readonly AuthService _authService;
        private readonly ImapMailService _imapMailService;

        public ImapController(AuthService authService, ImapMailService imapMailService)
        {
            _authService = authService;
            _imapMailService = imapMailService;
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

        [HttpGet]
        [Route("GetFolders")]
        public async Task<IActionResult> GetFolders([FromQuery]string token)
        {
            try
            {
                var folders = await _imapMailService.GetFoldersAsync(token);
                return StatusCode(200, new FoldersResponse() { Status = 200, Code = 0, Count = folders.Count, Folders = folders });
            }
            catch (Exception e)
            {
                var statusCode = 500;
                return StatusCode(statusCode, new ErrorResponse() { Status = 500, DeveloperMessage = e.Source, UserMessage = e.Message, MoreInfo = e.HelpLink, ErrorCode = e.HResult });
            }
        }

        [HttpGet]
        [Route("GetMessages")]
        public async Task<IActionResult> GetMessagesAsync([FromQuery]string token, int min, int max, string folderName)
        {
            try
            {
                var messages = await _imapMailService.GetFolderMessagesAsync(token, min, max, folderName);
                //return StatusCode(200, new FoldersResponse() { Status = 200, Code = 0, Count = folders.Count, Folders = folders });
                //var sz = JsonConvert.SerializeObject(messages);
                return StatusCode(200, new EnvelopesResponse() { Status = 200, Code = 0, Count = messages.Count, Envelopes = messages.ToList()});
            }
            catch (Exception e)
            {
                var statusCode = 500;
                return StatusCode(statusCode, new ErrorResponse() { Status = 500, DeveloperMessage = e.Source, UserMessage = e.Message, MoreInfo = e.HelpLink, ErrorCode = e.HResult });
            }
        }
    }
}