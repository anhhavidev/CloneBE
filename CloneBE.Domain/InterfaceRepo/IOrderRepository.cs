using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using Microsoft.EntityFrameworkCore.Storage;

namespace CloneBE.Domain.InterfaceRepo
{
    public interface IOrderRepository
    {
        Task<Order?> CheckUserOrderAsync(string userId);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderStatusAsync(int orderId, string newStatus);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<bool> DeleteOrderAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
}
