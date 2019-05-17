using System.Threading.Tasks;
using MailerWeb.Server.Services;
using MailerWeb.Shared.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MailerWeb.Server.Controllers
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

        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessageAsync([FromBody] SendMessageData data)
        {
            await _smtpMailService.SendEmailAsync(data.Token, data.From, data.To, data.Subject, data.HtmlBody);
            return StatusCode(204);
        }
    }
}