using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Models.Requests;
using MailerWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MailerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmtpController : ControllerBase
    {
        private readonly ISmtpMailService _smtpMailService;

        public SmtpController(ISmtpMailService smtpMailService)
        {
            _smtpMailService = smtpMailService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(404);
        }

        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessageAsync([FromBody] SendMessageData data)
        {
            await _smtpMailService.SendEmailAsync(data.Token, data.From, data.To, data.Subject, data.HtmlBody);
            return StatusCode(204);
        }

    }
}