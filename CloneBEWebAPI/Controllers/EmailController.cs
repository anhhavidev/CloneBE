using CloneBE.Application.DTO;
using CloneBE.Application.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloneBEWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ISendMailService _sendMailService;

        public EmailController(ISendMailService sendMailService)
        {
            _sendMailService = sendMailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] MailContent mailContent)
        {
            if (mailContent == null || string.IsNullOrEmpty(mailContent.To))
            {
                return BadRequest("Invalid email request");
            }

            await _sendMailService.SendMail(mailContent);
            return Ok("Email sent successfully!");
        }
    }
}
