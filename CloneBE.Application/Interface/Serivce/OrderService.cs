using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CloneBE.Application.DTO;
using CloneBE.Application.DTO.Request;
using CloneBE.Application.Helper;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;

namespace CloneBE.Application.Interface.Serivce
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork1 unitOfWork1;
        private readonly IMapper mapper;
        private readonly ISendMailService emailService; // thêm dòng này
        public OrderService(IUnitOfWork1 unitOfWork1, IMapper mapper, ISendMailService emailService)
        {
            this.unitOfWork1 = unitOfWork1;
            this.mapper = mapper;
            this.emailService = emailService;
        }

     public async Task<bool> PlaceOrderAsync(ClaimsPrincipal user, OrderRequest request)
{
    try
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return false;

        // Kiểm tra thông tin bắt buộc
        if (string.IsNullOrWhiteSpace(request.PhoneNumber) || string.IsNullOrWhiteSpace(request.ShippingAddress))
            return false;

        var cart = await unitOfWork1.cartRepo.CheckUserCart(userId);
        if (cart?.cartItems == null || !cart.cartItems.Any()) return false;

        double totalAmount = cart.cartItems.Sum(ci => ci.quanlity * ci.price);

        using (var transaction = await unitOfWork1.OrderRepo.BeginTransactionAsync())
        {
            try
            {
                var newOrder = new Order
                {
                    UserId = userId,
                    TotalAmount = totalAmount,
                    Status = "Pending",
                    PhoneNumber = request.PhoneNumber,
                    ShippingAddress = request.ShippingAddress,
                    OrderDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    OrderDetails = cart.cartItems.Select(ci => new OrderDetail
                    {
                        ProductId = ci.Productid,
                        Quantity = ci.quanlity,
                        UnitPrice = ci.price
                    }).ToList()
                };

                await unitOfWork1.OrderRepo.CreateOrderAsync(newOrder);

                // Cập nhật tồn kho
                foreach (var cartItem in cart.cartItems)
                {
                    var product = await unitOfWork1.ProductRepo.GetProductsByIdsAsync(cartItem.Productid);
                    if (product == null || product.stock < cartItem.quanlity)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }

                    product.stock -= cartItem.quanlity;
                    unitOfWork1.ProductRepo.Update(product);
                }

                // Xoá cart items
                var cartItemIds = cart.cartItems.Select(ci => ci.CartitemId).ToList();
                await unitOfWork1.cartRepo.RemoveCartItemsByIdsAsync(cartItemIds);

                await unitOfWork1.SaveChangesAsync();
                await transaction.CommitAsync();
                        await emailService.SendEmailAsync( userId,
            "Đơn hàng đã được tạo thành công",
            $"Đơn hàng của bạn đã được tạo với tổng tiền là {totalAmount} VND. Cảm ơn bạn đã mua hàng!");
                        return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
    catch
    {
        return false;
    }
}

        public async Task<OrderDTO?> GetOrderByIdAsync(int orderId)
        {
            var order = await unitOfWork1.OrderRepo.GetOrderByIdAsync(orderId);
            return mapper.Map<OrderDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByUserAsync(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orders = await unitOfWork1.OrderRepo.GetOrdersByUserIdAsync(userId);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<bool> ApproveOrderAsync(int orderId)
        {
            var order = await unitOfWork1.OrderRepo.GetOrderByIdAsync(orderId);
            if (order == null || order.Status != "Paid") return false;

            order.Status = "Approved";
            order.UpdatedAt = DateTime.UtcNow;
            await unitOfWork1.SaveChangesAsync();
            await emailService.SendEmailAsync(
    order.UserId,
    "Đơn hàng đã được duyệt",
    $"Đơn hàng với mã {order.OrderId} đã được duyệt và đang được xử lý.");

            return true;
        }
        public async Task<bool> CancelOrderAsync(int orderId, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return false;

            var order = await unitOfWork1.OrderRepo.GetOrderByIdAsync(orderId);
            if (order == null || order.UserId != userId) return false;

            // Chỉ cho hủy đơn nếu trạng thái là Pending hoặc Paid (chưa Approved)
            if (order.Status == "Approved" || order.Status == "Cancelled")
                return false;

            order.Status = "Cancelled";
            order.UpdatedAt = DateTime.UtcNow;

            // Nếu bạn cần hoàn trả kho hàng khi hủy
            foreach (var detail in order.OrderDetails)
            {
                var product = await unitOfWork1.ProductRepo.GetProductsByIdsAsync(detail.ProductId);
                if (product != null)
                {
                    product.stock += detail.Quantity;
                    unitOfWork1.ProductRepo.Update(product);
                }
            }

            await unitOfWork1.SaveChangesAsync();

            // Gửi email thông báo hủy đơn
            await emailService.SendEmailAsync(
                userId,
                "Đơn hàng đã bị hủy",
                $"Đơn hàng của bạn với mã {order.OrderId} đã bị hủy.");

            return true;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await unitOfWork1.OrderRepo.GetOrderByIdAsync(orderId);
            if (order == null || order.Status == "Approved") return false;

            await unitOfWork1.OrderRepo.DeleteOrderAsync(orderId);
            await unitOfWork1.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await unitOfWork1.OrderRepo.GetOrdersByUserIdAsync(userId);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            try
            {
                await unitOfWork1.OrderRepo.UpdateOrderStatusAsync(orderId, newStatus);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> PayOrderAsync(int orderId, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var order = await unitOfWork1.OrderRepo.GetOrderByIdAsync(orderId);

            if (order == null || order.UserId != userId || order.Status != "Pending")
                return false;

            order.Status = "Paid";
            order.UpdatedAt = DateTime.UtcNow;
            await unitOfWork1.SaveChangesAsync();
            await emailService.SendEmailAsync(
    userId,
    "Đơn hàng đã được thanh toán",
    $"Đơn hàng với mã {order.OrderId} đã được thanh toán thành công.");

            return true;
        }
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await unitOfWork1.OrderRepo.GetAllOrdersAsync();
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

    }
}
