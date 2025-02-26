using CloneBE.Application.DTO;
using CloneBE.Application.Interface.Serivce;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CloneBEWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }


        /// <summary>
        /// Đăng ký tài khoản mới
        /// </summary>
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await accountService.SignUpAsync(model);

            if (result.Succeeded)
            {
                return Ok(new { message = "Đăng ký thành công!" });
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Đăng nhập và lấy JWT Token
        /// </summary>
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await accountService.SignInAsync(model);
            if (response == null)
            {
                return Unauthorized(new { message = "Sai email hoặc mật khẩu!" });
            }

            return Ok(response);
        }
    }
}
