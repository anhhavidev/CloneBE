using CloneBE.Application.DTO;
using CloneBE.Application.DTO.Request;
using CloneBE.Application.Interface.Serivce;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Thông tin không hợp lệ!" });

            try
            {
                var message = await accountService.ForgotPasswordAsync(model.Email, Request.Scheme, Request.Host.ToString());
                return Ok(new { message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi, vui lòng thử lại sau!" });
            }
        }

    }
}
