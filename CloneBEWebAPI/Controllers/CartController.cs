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

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] ProductCartRequest request)
        {
            var result = await _cartService.AddProductToCart(request, User);
            if (!result)
            {
                return BadRequest("Không thể thêm sản phẩm vào giỏ hàng. Có thể sản phẩm hết hàng.");
            }
            return Ok("Đã thêm vào giỏ hàng thành công.");
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetCartItems()
        {
            var cartItems = await _cartService.GetallCartItem(User);
            if (cartItems == null)
            {
                return NotFound("Không tìm thấy giỏ hàng.");
            }
            return Ok(cartItems);
        }

        [HttpDelete("item/{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var result = await _cartService.DeleteCartItem(id, User);
            if (!result)
            {
                return NotFound("Không tìm thấy mục giỏ hàng cần xoá.");
            }
            return Ok("Đã xoá sản phẩm khỏi giỏ hàng.");
        }

        [HttpPut("item/{id}/quantity")]
        public async Task<IActionResult> UpdateCartItemQuantity(int id, [FromQuery] int quantity)
        {
            var result = await _cartService.UpdateCartItemQuantity(id, quantity, User);
            if (!result)
            {
                return BadRequest("Không thể cập nhật số lượng.");
            }
            return Ok("Đã cập nhật số lượng sản phẩm trong giỏ hàng.");
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetCartTotal()
        {
            var total = await _cartService.GetCartTotal(User);
            return Ok(new { Total = total });
        }
    }
}