using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Server.Services;
using MailerWeb.Shared.Models;
using MailerWeb.Shared.Models.Requests;
using MailerWeb.Shared.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MailerWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("AddSignature")]
        public async Task<IActionResult> AddSignatureAsync([FromBody] AddSignatureData data)
        {
            var signature = await _userService.AddSignatureAsync(data.Token, data.Signature);
            return StatusCode(200,
                new SignaturesResponse
                    {Code = 0, Status = 200, Count = 1, Signatures = new List<Signature> {signature}});
        }

        [HttpGet]
        [Route("GetSignature")]
        public async Task<IActionResult> GetSignatureAsync([FromQuery] string token, int signatureId)
        {
            var signature = await _userService.GetSignatureAsync(token, signatureId);
            return StatusCode(200,
                new SignaturesResponse
                    {Code = 0, Status = 200, Count = 1, Signatures = new List<Signature> {signature}});
        }

        [HttpGet]
        [Route("GetSignatures")]
        public async Task<IActionResult> GetSignatures([FromQuery] string token)
        {
            var signatures = await _userService.GetSignaturesAsync(token);
            return StatusCode(200,
                new SignaturesResponse
                    {Code = 0, Status = 200, Count = signatures.Count, Signatures = signatures.ToList()});
        }

        [HttpDelete]
        [Route("DeleteSignature")]
        public async Task<IActionResult> DeleteSignatureAsync([FromQuery] string token, int signatureId)
        {
            await _userService.DeleteSignatureAsync(token, signatureId);
            return StatusCode(204);
        }

        [HttpPut]
        [Route("EditSignature")]
        public async Task<IActionResult> EditSignatureAsync([FromBody] EditSignatureData data)
        {
            var signature = await _userService.EditSignatureAsync(data.Token, data.SignatureId, data.NewSignature);
            return StatusCode(200,
                new SignaturesResponse
                    {Code = 0, Status = 200, Count = 1, Signatures = new List<Signature> {signature}});
        }
    }
}