using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CloneBE.Application.DTO;
using CloneBE.Application.DTO.Request;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;

namespace CloneBE.Application.Interface.Serivce
{
    public class OrderService :IOrderService
    {
        private readonly IUnitOfWork1 unitOfWork1;
        private readonly IMapper mapper;

        public OrderService(IUnitOfWork1 unitOfWork1,IMapper mapper)
        {
            this.unitOfWork1 = unitOfWork1;
            this.mapper = mapper;
        }


        public async Task<bool> PlaceOrderAsync(ClaimsPrincipal user, OrderRequest request)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return false;

            var cart = await unitOfWork1.cartRepo.CheckUserCart(userId);
            if (cart == null || !cart.cartItems.Any()) return false;

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
                        PhoneNumber=request.PhoneNumber,
                        ShippingAddress=request.ShippingAddress,
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

                    // Cập nhật tồn kho trong cùng 1 transaction
                    foreach (var cartItem in cart.cartItems)
                    {
                        var product = await unitOfWork1.ProductRepo.GetProductsByIdsAsync(cartItem.Productid);
                        if (product == null || product.stock < cartItem.quanlity)
                        {
                            await transaction.RollbackAsync();
                            return false; // Hủy giao dịch nếu tồn kho không đủ
                        }

                        // Trừ số lượng hàng trong kho
                        product.stock -= cartItem.quanlity;
                         unitOfWork1.ProductRepo.Update(product);
                    }

                    var cartItemIds = cart.cartItems.Select(ci => ci.CartitemId).ToList();
                    await unitOfWork1.cartRepo.RemoveCartItemsByIdsAsync(cartItemIds);


                    await unitOfWork1.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }


        public async Task<OrderDTO?> GetOrderByIdAsync(int orderId)
        {
           var order= await unitOfWork1.OrderRepo.GetOrderByIdAsync(orderId);
            return mapper.Map<OrderDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(string userId)
        {  
            
            var order = await unitOfWork1.OrderRepo.GetOrdersByUserIdAsync(userId);
            return mapper.Map<IEnumerable<OrderDTO >> (order);
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
    }
}
