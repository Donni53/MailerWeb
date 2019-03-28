using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Models;
using MailerWeb.Services;
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
        [Route("Auth")]
        public async Task<IActionResult> Auth([FromBody]User user)
        {
            try
            {
                await _authService.Auth(user);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}