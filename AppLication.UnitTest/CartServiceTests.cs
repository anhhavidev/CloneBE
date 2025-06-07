using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CloneBE.Application.DTO.Request;
using CloneBE.Application.DTO;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;
using Moq;

namespace AppLication.UnitTest
{
    public class CartServiceTests
    {
        private readonly Mock<IUnitOfWork1> mockUnitOfWork;
        private readonly Mock<IMapper> mockMapper;
        private readonly CartService cartService;

        public CartServiceTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork1>();
            mockMapper = new Mock<IMapper>();
            cartService = new CartService(mockUnitOfWork.Object, mockMapper.Object);
        }

        // Tạo thông tin người dùng giả để kiểm thử
        private ClaimsPrincipal GetClaimsPrincipal(string userId)
        {
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId)
        }, "mock");
            return new ClaimsPrincipal(identity);
        }

        // Test: Thêm sản phẩm vào giỏ hàng khi chưa có giỏ hàng
        [Fact]
        public async Task AddProductToCart_Thêm_Mới_Thành_Công_Khi_Chưa_Có_Giỏ()
        {
            var userId = "user1";
            var claims = GetClaimsPrincipal(userId);
            var request = new ProductCartRequest { productId = 1, quantity = 2 };
            var product = new Product { ProductId = 1, stock = 5, Price = 100 };

            mockUnitOfWork.Setup(x => x.cartRepo.CheckUserCart(userId)).ReturnsAsync((Cart)null);
            mockUnitOfWork.Setup(x => x.ProductRepo.GetProductsByIdsAsync(1)).ReturnsAsync(product);
            mockUnitOfWork.Setup(x => x.cartRepo.CheckCartItem(It.IsAny<int>(), 1)).ReturnsAsync((CartItem)null);
            mockUnitOfWork.Setup(x => x.cartRepo.AddCart(It.IsAny<Cart>())).Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(x => x.cartRepo.addCartItem(It.IsAny<CartItem>())).Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var result = await cartService.AddProductToCart(request, claims);

            Assert.True(result);
        }

        // Test: Không thêm được sản phẩm khi không có thông tin người dùng
        [Fact]
        public async Task AddProductToCart_Trả_Về_False_Khi_Không_Có_Claims()
        {
            var claims = new ClaimsPrincipal(); // không có user
            var request = new ProductCartRequest { productId = 1, quantity = 2 };
            var result = await cartService.AddProductToCart(request, claims);
            Assert.False(result);
        }

        // Test: Không thêm được sản phẩm khi số lượng vượt quá tồn kho
        [Fact]
        public async Task AddProductToCart_Trả_Về_False_Khi_Số_Lượng_Muốn_Mua_Vượt_Tồn_Kho()
        {
            var userId = "user1";
            var claims = GetClaimsPrincipal(userId);
            var product = new Product { ProductId = 1, stock = 1, Price = 100 };

            mockUnitOfWork.Setup(x => x.cartRepo.CheckUserCart(userId)).ReturnsAsync(new Cart { CartId = 10, UserId = userId });
            mockUnitOfWork.Setup(x => x.ProductRepo.GetProductsByIdsAsync(1)).ReturnsAsync(product);

            var result = await cartService.AddProductToCart(new ProductCartRequest { productId = 1, quantity = 5 }, claims);

            Assert.False(result);
        }

        // Test: Lấy danh sách sản phẩm trong giỏ hàng
        //[Fact]
        //public async Task GetAllCartItem_Trả_Về_Danh_Sách_Đúng()
        //{
        //    var userId = "user1";
        //    var claims = GetClaimsPrincipal(userId);
        //    var cartItems = new List<CartItem> { new CartItem { Productid = 1, quanlity = 2, price = 50 } };

        //    mockUnitOfWork.Setup(x => x.cartRepo.GetAllItem(userId)).ReturnsAsync(cartItems);
        //    mockMapper.Setup(m => m.Map<IEnumerable<CartItemDTO>>(cartItems)).Returns(new List<CartItemDTO>
        //{
        //    new CartItemDTO { Productid = 1, quanlity = 2, price = 50 }
        //});

        //    var result = await cartService.GetallCartItem(claims);

        //    Assert.Single(result);
        //}

        // Test: Xóa sản phẩm khi là chủ sở hữu giỏ hàng
        [Fact]
        public async Task DeleteCartItem_Xóa_Thành_Công_Khi_Là_Chủ_Sở_Hữu()
        {
            var userId = "user1";
            var claims = GetClaimsPrincipal(userId);
            var item = new CartItem { CartitemId = 1, Cart = new Cart { UserId = userId } };

            mockUnitOfWork.Setup(x => x.cartRepo.GetCartItemById(1)).ReturnsAsync(item);
            mockUnitOfWork.Setup(x => x.cartRepo.deletecartitem(1)).ReturnsAsync(true);
            mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1); 

              var result = await cartService.DeleteCartItem(1, claims);

            Assert.True(result);
        }

        // Test: Không xóa được sản phẩm không thuộc người dùng
        [Fact]
        public async Task DeleteCartItem_Trả_Về_False_Khi_Không_Phải_Của_User()
        {
            var userId = "user1";
            var claims = GetClaimsPrincipal(userId);
            var item = new CartItem { CartitemId = 1, Cart = new Cart { UserId = "anotherUser" } };

            mockUnitOfWork.Setup(x => x.cartRepo.GetCartItemById(1)).ReturnsAsync(item);

            var result = await cartService.DeleteCartItem(1, claims);

            Assert.False(result);
        }

        // Test: Cập nhật số lượng khi hợp lệ và tồn kho đủ
        [Fact]
        public async Task UpdateCartItemQuantity_Cập_Nhật_Thành_Công_Khi_Hợp_Lệ()
        {
            var userId = "user1";
            var claims = GetClaimsPrincipal(userId);
            var item = new CartItem { CartitemId = 1, Productid = 1, Cart = new Cart { UserId = userId } };
            var product = new Product { ProductId = 1, stock = 10 };

            mockUnitOfWork.Setup(x => x.cartRepo.GetCartItemById(1)).ReturnsAsync(item);
            mockUnitOfWork.Setup(x => x.ProductRepo.GetProductsByIdsAsync(1)).ReturnsAsync(product);
            mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var result = await cartService.UpdateCartItemQuantity(1, 5, claims);

            Assert.True(result);
        }

        // Test: Không cập nhật được khi tồn kho không đủ
        [Fact]
        public async Task UpdateCartItemQuantity_Trả_Về_False_Khi_Tồn_Kho_Không_Đủ()
        {
            var userId = "user1";
            var claims = GetClaimsPrincipal(userId);
            var item = new CartItem { CartitemId = 1, Productid = 1, Cart = new Cart { UserId = userId } };
            var product = new Product { ProductId = 1, stock = 2 };

            mockUnitOfWork.Setup(x => x.cartRepo.GetCartItemById(1)).ReturnsAsync(item);
            mockUnitOfWork.Setup(x => x.ProductRepo.GetProductsByIdsAsync(1)).ReturnsAsync(product);

            var result = await cartService.UpdateCartItemQuantity(1, 5, claims);

            Assert.False(result);
        }

        // Test: Tính tổng tiền của giỏ hàng
        [Fact]
        public async Task GetCartTotal_Tính_Tổng_Đúng()
        {
            var userId = "user1";
            var claims = GetClaimsPrincipal(userId);
            var items = new List<CartItem>
        {
            new CartItem { price = 100, quanlity = 2 },
            new CartItem { price = 50, quanlity = 3 }
        };

            mockUnitOfWork.Setup(x => x.cartRepo.GetAllItem(userId)).ReturnsAsync(items);

            var result = await cartService.GetCartTotal(claims);

            Assert.Equal(100 * 2 + 50 * 3, result);
        }
    }
}
