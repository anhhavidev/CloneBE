using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using CloneBE.Infraction.Presistences;
using CloneBE.Infraction.Repo;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InfraStructure.UnitTest
{
    public class OrderRepositoryTests
    {
        private OrderRepository CreateRepository(out Databasese context)
        {
            var options = new DbContextOptionsBuilder<Databasese>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // mỗi test 1 db riêng
                .Options;
            context = new Databasese(options);
            return new OrderRepository(context);
        }

        [Fact]
        public async Task CheckUserOrderAsync_ReturnsLatestOrder()
        {
            // Arrange
            var userId = "user123";
            var orders = new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    UserId = userId,
                    OrderDate = DateTime.UtcNow.AddDays(-2),
                    PhoneNumber = "0123456789",
                    ShippingAddress = "123 Street"
                },
                new Order
                {
                    OrderId = 2,
                    UserId = userId,
                    OrderDate = DateTime.UtcNow,
                    PhoneNumber = "0123456789",
                    ShippingAddress = "123 Street"
                }
            };

            var repo = CreateRepository(out var context);

            context.orders.AddRange(orders);
            await context.SaveChangesAsync();

            // Act
            var result = await repo.CheckUserOrderAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.OrderId);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnsOrderWithDetails()
        {
            // Arrange
            var orderId = 1;
            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail { OrderDetailId = 1, OrderId = orderId },
                new OrderDetail { OrderDetailId = 2, OrderId = orderId }
            };
            var order = new Order
            {
                OrderId = orderId,
                UserId = "user123",
                PhoneNumber = "0123456789",
                ShippingAddress = "123 Street",
                OrderDetails = orderDetails
            };

            var repo = CreateRepository(out var context);

            context.orders.Add(order);
            await context.SaveChangesAsync();

            // Act
            var result = await repo.GetOrderByIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.OrderId);
            Assert.NotNull(result.OrderDetails);
            Assert.Equal(2, result.OrderDetails.Count);
        }

        [Fact]
        public async Task DeleteOrderAsync_DeletesOrderAndDetails()
        {
            // Arrange
            var orderId = 1;
            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail { OrderDetailId = 1, OrderId = orderId },
                new OrderDetail { OrderDetailId = 2, OrderId = orderId }
            };
            var order = new Order
            {
                OrderId = orderId,
                UserId = "user123",
                PhoneNumber = "0123456789",
                ShippingAddress = "123 Street",
                OrderDetails = orderDetails
            };

            var repo = CreateRepository(out var context);

            context.orders.Add(order);
            await context.SaveChangesAsync();

            // Act
            var result = await repo.DeleteOrderAsync(orderId);

            // Assert
            Assert.True(result);

            // Kiểm tra order và chi tiết đã bị xóa khỏi database
            var deletedOrder = await context.orders.FindAsync(orderId);
            Assert.Null(deletedOrder);

            var deletedDetails = await context.orderDetails
                .AnyAsync(d => d.OrderId == orderId);
            Assert.False(deletedDetails);
        }
    }
}
