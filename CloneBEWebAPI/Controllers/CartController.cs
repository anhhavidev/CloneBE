using CloneBE.Application.DTO.Request;
using CloneBE.Application.Interface.Serivce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloneBEWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("gETCARRTITEM")]
        //public Task<IActionResult> Get()
        //{


        //}

        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng
        /// </summary>
        [HttpPost("add")]
        public async Task<IActionResult> AddProductToCart([FromBody] ProductCartRequest cartRequest)
        {
            var user = User; // Lấy thông tin người dùng từ token
            var result = await _cartService.AddProductToCart(cartRequest, user);
            if (!result)
                return BadRequest("Không thể thêm sản phẩm vào giỏ hàng. Có thể hết hàng hoặc lỗi khác.");

            return Ok("Sản phẩm đã được thêm vào giỏ hàng.");
        }

        /// <summary>
        /// Xóa sản phẩm khỏi giỏ hàng
        /// </summary>
        [HttpDelete("delete/{cartItemId}")]
        public async Task<IActionResult> DeleteCartItem(int cartItemId)
        {
            var result = await _cartService.DeleteCartItem(cartItemId);
            if (!result)
                return BadRequest("Không thể xóa sản phẩm khỏi giỏ hàng.");

            return Ok("Sản phẩm đã được xóa khỏi giỏ hàng.");
        }
    }
}
