using CloneBE.Application.DTO.Request;
using CloneBE.Application.Interface.Serivce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloneBEWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Đặt hàng
        [HttpPost("place")]
        [Authorize]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest request)
        {
            var result = await _orderService.PlaceOrderAsync(User, request);
            return result ? Ok("Order placed successfully") : BadRequest("Failed to place order");
        }

        // Lấy đơn của chính người dùng hiện tại
        [HttpGet("my-orders")]
        [Authorize]
        public async Task<IActionResult> GetMyOrders()
        {
            var orders = await _orderService.GetOrdersByUserAsync(User);
            return Ok(orders);
        }

        // Admin lấy đơn của người khác
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        // Lấy đơn theo mã đơn
        [HttpGet("{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            return order == null ? NotFound() : Ok(order);
        }

        // Thanh toán đơn
        [HttpPost("pay/{orderId}")]
        [Authorize]
        public async Task<IActionResult> PayOrder(int orderId)
        {
            var result = await _orderService.PayOrderAsync(orderId, User);
            return result ? Ok("Order paid") : BadRequest("Cannot pay this order");
        }

        // Admin duyệt đơn
        [HttpPost("approve/{orderId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ApproveOrder(int orderId)
        {
            var result = await _orderService.ApproveOrderAsync(orderId);
            return result ? Ok("Order approved") : BadRequest("Approval failed");
        }

        // Admin cập nhật trạng thái đơn
        [HttpPut("update-status/{orderId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromQuery] string newStatus)
        {
            var result = await _orderService.UpdateOrderStatusAsync(orderId, newStatus);
            return result ? Ok("Status updated") : BadRequest("Update failed");
        }

        // Người dùng huỷ đơn nếu chưa duyệt
        [HttpDelete("{orderId}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var result = await _orderService.DeleteOrderAsync(orderId);
            return result ? Ok("Order deleted") : BadRequest("Cannot delete order");
        }
    }
}
