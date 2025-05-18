using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO;
using CloneBE.Application.DTO.Request;
using CloneBE.Domain.EF;

namespace CloneBE.Application.Interface.Serivce
{
    public interface IOrderService
    {
        Task<bool> PlaceOrderAsync(ClaimsPrincipal user, OrderRequest request);
        Task<OrderDTO?> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(string userId);
        Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus);
        Task<bool> PayOrderAsync(int orderId, ClaimsPrincipal user);
        Task<bool> ApproveOrderAsync(int orderId);
      Task<bool> DeleteOrderAsync(int orderId);
        
     Task<IEnumerable<OrderDTO>> GetOrdersByUserAsync(ClaimsPrincipal user);



    }
}
