using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using CloneBE.Infraction.Presistences;
using CloneBE.Infraction.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace InfraStructure.UnitTest
{
    // Lớp kiểm thử tích hợp cho OrderRepository sử dụng xUnit
    public class OrderRepositoryIntegrationTests : IAsyncLifetime
    {
        private readonly Databasese _context;       // DbContext kết nối tới cơ sở dữ liệu
        private readonly OrderRepository _repo;     // Repository cần kiểm thử
        private IDbContextTransaction _transaction; // Giao dịch để rollback sau mỗi test

        // Constructor - cấu hình DbContext và repository
        public OrderRepositoryIntegrationTests()
        {
            // Cấu hình DbContext kết nối với database test
            var options = new DbContextOptionsBuilder<Databasese>()
                .UseSqlServer("Data Source=DESKTOP-MEHO8LU;Initial Catalog=CloneBE_Test;Integrated Security=True;Encrypt=False")
                .Options;

            _context = new Databasese(options);
            _repo = new OrderRepository(_context);
        }

        // Hàm chạy trước mỗi test, khởi tạo transaction
        public Task InitializeAsync()
        {
            _transaction = _context.Database.BeginTransaction();
            return Task.CompletedTask;
        }

        // Hàm chạy sau mỗi test, rollback và giải phóng tài nguyên
        public async Task DisposeAsync()
        {
            await _transaction.RollbackAsync();        // Quay lui dữ liệu để đảm bảo test độc lập
            await _transaction.DisposeAsync();         // Giải phóng transaction
            await _context.DisposeAsync();             // Giải phóng DbContext
        }

        // Kiểm thử CheckUserOrderAsync trả về đơn hàng mới nhất của người dùng
        [Fact]
        public async Task CheckUserOrderAsync_ReturnsLatestOrder()
        {
            var userId = "user123";

            // Tìm người dùng, nếu chưa có thì tạo mới
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                user = new AppUser
                {
                    Id = userId,
                    UserName = "user123",
                    NormalizedUserName = "USER123",
                    Email = "user123@example.com",
                    NormalizedEmail = "USER123@EXAMPLE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    FullName = "Test User",
                    Address = "123 Test Street"
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            // Tạo danh sách 2 đơn hàng với thời gian khác nhau
            var orders = new List<Order>
            {
                new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.UtcNow.AddDays(-2), // Đơn hàng cũ hơn
                    PhoneNumber = "0123456789",
                    ShippingAddress = "123 Street"
                },
                new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.UtcNow,             // Đơn hàng mới hơn
                    PhoneNumber = "0123456789",
                    ShippingAddress = "123 Street"
                }
            };

            // Thêm đơn hàng vào database
            await _context.orders.AddRangeAsync(orders);
            await _context.SaveChangesAsync();

            // Gọi phương thức kiểm tra đơn hàng mới nhất
            var result = await _repo.CheckUserOrderAsync(userId);

            // Kiểm tra kết quả
            Assert.NotNull(result);
            Assert.Equal(orders[1].OrderId, result.OrderId); // So sánh với đơn hàng mới nhất
        }

        // Kiểm thử GetOrderByIdAsync trả về đơn hàng kèm theo chi tiết đơn hàng
        [Fact]
        public async Task GetOrderByIdAsync_ReturnsOrderWithDetails()
        {
            // Tạo một người dùng giả định
            var user = new AppUser
            {
                Id = "user123",
                UserName = "user123",
                NormalizedUserName = "USER123",
                Email = "user123@example.com",
                NormalizedEmail = "USER123@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FullName = "Test User",
                Address = "123 Test Street" // Không được để null
            };

            // Nếu người dùng chưa tồn tại thì thêm mới
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            // Tạo đơn hàng với 2 chi tiết đơn hàng
            var order = new Order
            {
                UserId = user.Id,
                PhoneNumber = "0123456789",
                ShippingAddress = "123 Street",
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail(),
                    new OrderDetail()
                }
            };

            _context.orders.Add(order);
            await _context.SaveChangesAsync();

            var orderId = order.OrderId;

            // Gọi repository để lấy đơn hàng
            var result = await _repo.GetOrderByIdAsync(orderId);

            // Kiểm tra kết quả trả về
            Assert.NotNull(result);
            Assert.Equal(orderId, result.OrderId);
            Assert.NotNull(result.OrderDetails);
            Assert.Equal(2, result.OrderDetails.Count); // Đảm bảo có đúng 2 chi tiết đơn hàng
        }

        // Kiểm thử DeleteOrderAsync để xóa đơn hàng và các chi tiết liên quan
        [Fact]
        public async Task DeleteOrderAsync_DeletesOrderAndDetails()
        {
            var userId = "user123";

            // Tạo user nếu chưa có
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                user = new AppUser
                {
                    Id = userId,
                    UserName = "user123",
                    NormalizedUserName = "USER123",
                    Email = "user123@example.com",
                    NormalizedEmail = "USER123@EXAMPLE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    FullName = "Test User",
                    Address = "123 Test Street"
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            // Tạo đơn hàng giả lập có 2 chi tiết đơn hàng
            var order = new Order
            {
                UserId = userId,
                PhoneNumber = "0123456789",
                ShippingAddress = "123 Street",
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail(),
                    new OrderDetail()
                }
            };

            await _context.orders.AddAsync(order);
            await _context.SaveChangesAsync();

            var orderId = order.OrderId;

            // Gọi phương thức xóa đơn hàng
            var result = await _repo.DeleteOrderAsync(orderId);

            // Kiểm tra kết quả trả về là true (xóa thành công)
            Assert.True(result);

            // Kiểm tra đơn hàng đã bị xóa khỏi database
            var deletedOrder = await _context.orders.FindAsync(orderId);
            Assert.Null(deletedOrder);

            // Kiểm tra không còn chi tiết đơn hàng liên quan
            var hasOrderDetails = await _context.orderDetails.AnyAsync(d => d.OrderId == orderId);
            Assert.False(hasOrderDetails);
        }
    }
}
