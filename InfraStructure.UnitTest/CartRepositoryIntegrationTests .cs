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
    // Lớp kiểm thử tích hợp cho CartRepository
    public class CartRepositoryIntegrationTests : IAsyncLifetime
    {
        private readonly Databasese _context;         // DbContext để thao tác với cơ sở dữ liệu
        private readonly CartRePo _repo;              // Repository của Cart dùng để gọi các hàm xử lý logic
        private IDbContextTransaction _transaction;   // Đối tượng quản lý giao dịch, dùng để rollback sau mỗi test

        // Hàm khởi tạo, thiết lập DbContext và Repository
        public CartRepositoryIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<Databasese>()
                .UseSqlServer("Data Source=DESKTOP-MEHO8LU;Initial Catalog=CloneBE_Test;Integrated Security=True;Encrypt=False")
                .Options;

            _context = new Databasese(options);
            _repo = new CartRePo(_context);
        }

        // Hàm được gọi trước mỗi test, dùng để bắt đầu transaction
        public async Task InitializeAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        // Hàm được gọi sau mỗi test, dùng để rollback dữ liệu đã thay đổi và giải phóng tài nguyên
        public async Task DisposeAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            await _context.DisposeAsync();
        }

        // Hàm hỗ trợ: đảm bảo người dùng tồn tại, nếu không thì tạo mới
        private async Task EnsureUserExists(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                user = new AppUser
                {
                    Id = userId,
                    UserName = userId,
                    NormalizedUserName = userId.ToUpper(),
                    Email = $"{userId}@example.com",
                    NormalizedEmail = $"{userId}@example.com".ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    FullName = "Test User",
                    Address = "123 Test Address"
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
        }

        // Hàm hỗ trợ: đảm bảo sản phẩm tồn tại, nếu không thì tạo mới sản phẩm và trả về ProductId
        private async Task<int> EnsureProductExists(int? productId = null)
        {
            Product product = null;

            if (productId.HasValue)
            {
                product = await _context.products.FindAsync(productId.Value);
                if (product != null)
                    return product.ProductId;
            }

            product = new Product
            {
                Name = $"Test Product {Guid.NewGuid()}",
                Price = 100,
                CategoryId = 1,
                LinkImagesPath = "default-image.jpg"
            };
            _context.products.Add(product);
            await _context.SaveChangesAsync();

            return product.ProductId;
        }

        // Kiểm thử thêm giỏ hàng và kiểm tra có tồn tại giỏ hàng cho user không
        [Fact]
        public async Task AddCart_And_CheckUserCart_ReturnsCart()
        {
            string userId = "user_test_1";

            await EnsureUserExists(userId);

            var cart = new Cart
            {
                UserId = userId
            };

            await _repo.AddCart(cart);
            await _context.SaveChangesAsync();

            var result = await _repo.CheckUserCart(userId);

            Assert.NotNull(result);               // Đảm bảo giỏ hàng không null
            Assert.Equal(userId, result.UserId);  // Đảm bảo giỏ hàng đúng với user đã tạo
        }

        // Kiểm thử thêm sản phẩm vào giỏ hàng và kiểm tra sản phẩm đã thêm đúng không
        [Fact]
        public async Task AddCartItem_And_CheckCartItem_ReturnsCorrectItem()
        {
            string userId = "user_test_2";
            await EnsureUserExists(userId);

            int productId = await EnsureProductExists();

            var cart = new Cart { UserId = userId };
            await _repo.AddCart(cart);
            await _context.SaveChangesAsync();

            var cartItem = new CartItem
            {
                CartId = cart.CartId,
                Productid = productId,
                quanlity = 1
            };

            await _repo.addCartItem(cartItem);
            await _context.SaveChangesAsync();

            var checkItem = await _repo.CheckCartItem(cart.CartId, productId);

            Assert.NotNull(checkItem);                            // Đảm bảo item không null
            Assert.Equal(cart.CartId, checkItem.CartId);          // Kiểm tra đúng CartId
            Assert.Equal(productId, checkItem.Productid);         // Kiểm tra đúng ProductId
        }

        // Kiểm thử xóa sản phẩm khỏi giỏ hàng
        [Fact]
        public async Task DeleteCartItem_DeletesItemSuccessfully()
        {
            string userId = "user_test_3";
            await EnsureUserExists(userId);

            int productId = await EnsureProductExists();

            var cart = new Cart { UserId = userId };
            await _repo.AddCart(cart);
            await _context.SaveChangesAsync();

            var cartItem = new CartItem
            {
                CartId = cart.CartId,
                Productid = productId,
                quanlity = 2
            };

            await _repo.addCartItem(cartItem);
            await _context.SaveChangesAsync();

            var itemId = cartItem.CartitemId;

            var deleteResult = await _repo.deletecartitem(itemId);

            Assert.True(deleteResult);                             // Kết quả xóa phải trả về true

            var checkDeleted = await _context.cartItems.FindAsync(itemId);
            Assert.Null(checkDeleted);                             // Sau khi xóa, item phải không còn trong database
        }

        // Kiểm thử lấy toàn bộ sản phẩm trong giỏ hàng của người dùng
        [Fact]
        public async Task GetAllItem_ReturnsAllCartItemsOfUser()
        {
            string userId = "user_test_4";

            await EnsureUserExists(userId);

            var cart = new Cart { UserId = userId };
            await _repo.AddCart(cart);
            await _context.SaveChangesAsync();

            int productId1 = await EnsureProductExists();
            int productId2 = await EnsureProductExists();

            var items = new List<CartItem>
            {
                new CartItem { CartId = cart.CartId, Productid = productId1, quanlity = 1 },
                new CartItem { CartId = cart.CartId, Productid = productId2, quanlity = 3 }
            };

            await _context.cartItems.AddRangeAsync(items);
            await _context.SaveChangesAsync();

            var allItems = await _repo.GetAllItem(userId);

            Assert.NotNull(allItems);                              // Đảm bảo không null
            Assert.Equal(2, ((List<CartItem>)allItems).Count);     // Đảm bảo có đúng 2 sản phẩm đã thêm
        }
    }
}
