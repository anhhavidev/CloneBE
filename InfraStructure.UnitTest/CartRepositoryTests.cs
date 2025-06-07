using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using CloneBE.Infraction.Presistences;
using CloneBE.Infraction.Repo;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InfraStructure.UnitTest
{
    public class CartRepositoryTests : IDisposable
    {
        private readonly Databasese _context;
        private readonly CartRePo _cartRepo;

        public CartRepositoryTests()
        {
            // Setup InMemory DbContext options với tên db riêng biệt cho test này
            var options = new DbContextOptionsBuilder<Databasese>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Đảm bảo db sạch mỗi test run
                .Options;

            _context = new Databasese(options);
            _cartRepo = new CartRePo(_context);
        }

        public void Dispose()
        {
            // Clean up db sau mỗi test
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task CheckCartItem_ReturnsCartItem_WhenExists()
        {
            // Arrange
            var cartItems = new List<CartItem>
            {
                new CartItem { CartitemId = 1, CartId = 1, Productid = 2 },
                new CartItem { CartitemId = 2, CartId = 1, Productid = 3 }
            };
            _context.cartItems.AddRange(cartItems);
            await _context.SaveChangesAsync();

            // Act
            var result = await _cartRepo.CheckCartItem(1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CartId);
            Assert.Equal(2, result.Productid);
        }

        [Fact]
        public async Task CheckUserCart_ReturnsCartWithItems_WhenExists()
        {
            // Arrange
            var userId = "user1";
            var carts = new List<Cart>
            {
                new Cart
                {
                    CartId = 1,
                    UserId = userId,
                    cartItems = new List<CartItem>
                    {
                        new CartItem { CartitemId = 1, CartId = 1, Productid = 10 }
                    }
                }
            };
            _context.carts.AddRange(carts);
            await _context.SaveChangesAsync();

            // Act
            var result = await _cartRepo.CheckUserCart(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.NotNull(result.cartItems);
            Assert.Single(result.cartItems);
        }

        [Fact]
        public async Task DeleteCartItem_ReturnsFalse_WhenItemNotFound()
        {
            // Arrange
            // Không thêm gì vào cartItems

            // Act
            var result = await _cartRepo.deletecartitem(99);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteCartItem_ReturnsTrue_AndRemovesItem_WhenFound()
        {
            // Arrange
            var cartItem = new CartItem { CartitemId = 1 };
            _context.cartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            // Act
            var result = await _cartRepo.deletecartitem(1);

            // Assert
            Assert.True(result);

            // Kiểm tra item đã bị xóa
            var itemInDb = await _context.cartItems.FindAsync(1);
            Assert.Null(itemInDb);
        }

        [Fact]
        public async Task GetAllItem_ReturnsItemsForUser()
        {
            // Arrange
            var userId = "user1";
            var carts = new List<Cart>
            {
                new Cart { CartId = 1, UserId = userId },
                new Cart { CartId = 2, UserId = "user2" }
            };
            _context.carts.AddRange(carts);
            await _context.SaveChangesAsync();

            var cartItems = new List<CartItem>
            {
                new CartItem { CartitemId = 1, CartId = 1, Cart = carts[0] },
                new CartItem { CartitemId = 2, CartId = 2, Cart = carts[1] }
            };
            _context.cartItems.AddRange(cartItems);
            await _context.SaveChangesAsync();

            // Act
            var result = await _cartRepo.GetAllItem(userId);

            // Assert
            Assert.Single(result);
            Assert.All(result, item => Assert.Equal(userId, item.Cart.UserId));
        }

        [Fact]
        public async Task GetCartItemById_ReturnsCartItemWithCart()
        {
            // Arrange
            var cart = new Cart { CartId = 1, UserId = "user1" };
            var cartItem = new CartItem { CartitemId = 1, CartId = 1, Cart = cart };
            _context.carts.Add(cart);
            _context.cartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            // Act
            var result = await _cartRepo.GetCartItemById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CartitemId);
            Assert.NotNull(result.Cart);
            Assert.Equal("user1", result.Cart.UserId);
        }
    }
}
