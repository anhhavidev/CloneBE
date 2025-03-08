using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;
using CloneBE.Infraction.Presistences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CloneBE.Infraction.Repo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Databasese _context;

        public OrderRepository(Databasese context)
        {
            _context = context;
        }

        public async Task<Order?> CheckUserOrderAsync(string userId)
        {
            return await _context.orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.orders
                .Include(o => o.OrderDetails) // Lấy luôn danh sách sản phẩm trong đơn
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderDetails)
              .ToListAsync();
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _context.orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var order = await _context.orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = newStatus;
                order.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

    
    }
}
