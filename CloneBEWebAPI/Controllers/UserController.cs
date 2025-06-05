using CloneBE.Application.DTO.Request;
using System.Security.Claims;
using CloneBE.Application.Interface;
using CloneBE.Application.Interface.Serivce;
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
        // Lấy thông tin người dùng hiện tại
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _userService.GetCurrentUserAsync(userId);
            if (user == null) return NotFound();

            return Ok(user); // Hoặc map sang DTO nếu cần
        }

        // Cập nhật thông tin người dùng hiện tại
        [HttpPut("me")]
        public async Task<IActionResult> UpdateCurrentUser(UpdateUserModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var result = await _userService.UpdateCurrentUserAsync(userId, model);
            if (!result) return NotFound();

            return Ok("Cập nhật thành công");
        }

    }
}
