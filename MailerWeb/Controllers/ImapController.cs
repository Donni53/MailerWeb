using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Models;
using MailerWeb.Models.Requests;
using MailerWeb.Models.Responses;
using MailerWeb.Services;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;

namespace MailerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImapController : ControllerBase
    {

        private readonly AuthService _authService;
        private readonly IImapMailService _imapMailService;

        public ImapController(AuthService authService, IImapMailService imapMailService)
        {
            _authService = authService;
            _imapMailService = imapMailService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(404);
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp([FromBody]User user)
        {
            var token = await _authService.SignUpAsync(user);
            return StatusCode(200, new TokenResponse() { Status = 200, Code = 0, Token = token });
        }


        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn([FromBody]SignInCredentials signInCredentials)
        {

            var token = await _authService.SignInAsync(signInCredentials);
            return StatusCode(200, new TokenResponse() { Status = 200, Code = 0, Token = token });

        }

        [HttpGet]
        [Route("GetFolders")]
        public async Task<IActionResult> GetFolders([FromQuery]string token)
        {

            var folders = await _imapMailService.GetFoldersAsync(token);
            return StatusCode(200, new FoldersResponse() { Status = 200, Code = 0, Count = folders.Count, Folders = folders });

        }

        [HttpGet]
        [Route("GetMessages")]
        public async Task<IActionResult> GetMessagesAsync([FromQuery]string token, int min, int max, string folderName)
        {
            var messages = await _imapMailService.GetFolderMessagesAsync(token, min, max, folderName);
            return StatusCode(200, new EnvelopesResponse() { Status = 200, Code = 0, Count = messages.Count, Envelopes = messages.ToList() });
        }

        [HttpGet]
        [Route("GetMessage")]
        public async Task<IActionResult> GetMessageAsync([FromQuery]string token, int index, string folderName, bool bodyText)
        {
            var message = await _imapMailService.GetMessageBodyAsync(token, folderName, index);
            return StatusCode(200, new MessageResponse(message, bodyText) { Status = 200, Code = 0 });
        }

        [HttpPost]
        [Route("CreateFolder")]
        public async Task<IActionResult> CreateFolderAsync([FromBody]CreateFolderData data)
        {
            var folders = await _imapMailService.CreateFolderAsync(data.Token, data.FolderName, data.AllFolders);
            return StatusCode(201, new FoldersResponse() {Status = 201, Code = 0, Count = folders.Count, Folders = folders});
        }

        [HttpPost]
        [Route("CreateSubfolder")]
        public async Task<IActionResult> CreateSubfolderAsync([FromBody]CreateSubfolderData data)
        {
            var folders = await _imapMailService.CreateSubfolderAsync(data.Token, data.FolderName, data.SubfolderName, data.AllFolders);
            return StatusCode(200, new FoldersResponse() { Status = 201, Code = 0, Count = folders.Count, Folders = folders});
        }


        [HttpDelete]
        [Route("DeleteFolder")]
        public async Task<IActionResult> DeleteFolderAsync([FromBody]DeleteFolderData data)
        {
            var folders = await _imapMailService.DeleteFolderAsync(data.Token, data.FolderName, data.AllFolders);
            return StatusCode(200, new FoldersResponse() { Status = 200, Code = 0, Count = folders.Count, Folders = folders });
        }

        [HttpDelete]
        [Route("DeleteMessages")]
        public async Task<IActionResult> DeleteMessagesAsync([FromBody]DeleteMessagesData data)
        {
            await _imapMailService.DeleteMessagesAsync(data.Token, data.IndexList, data.FolderName);
            return StatusCode(204);
        }

        [HttpPut]
        [Route("MoveMessages")]
        public async Task<IActionResult> MoveMessagesAsync([FromBody] MoveData data)
        {
            await _imapMailService.MoveMessagesAsync(data.Token, data.IndexList, data.FolderName, data.DestFolderName);
            return StatusCode(204);
        }


        [HttpPut]
        [Route("MarkMessages")]
        public async Task<IActionResult> MarkMessagesAsync([FromBody]MarkData data)
        {
            await _imapMailService.MarkMessages(data.Token, data.IndexList, data.FolderName, data.Flag);
            return StatusCode(204);
        }
    }
}