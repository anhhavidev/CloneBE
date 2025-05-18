using CloneBE.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloneBEWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IIUserService _userService;

        public UserController(IIUserService userService)
        {
            _userService = userService;
        }

        // API lấy danh sách người dùng
        [HttpGet]
        [Authorize(Roles = "admin")] // Chỉ cho phép admin gọi API này
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync(); // Gọi service để lấy người dùng
            return Ok(users);
        } 
    }
}
