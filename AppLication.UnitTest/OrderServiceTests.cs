using AutoMapper;
using CloneBE.Application.DTO.Request;
using CloneBE.Application.Interface.Serivce;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using System.Security.Claims;

public class OrderServiceTests
{
    // Mock các đối tượng phụ thuộc
    private readonly Mock<IUnitOfWork1> _mockUnitOfWork = new();
    private readonly Mock<IMapper> _mockMapper = new();
    private readonly OrderService _orderService;

    //public OrderServiceTests()
    //{
    //    // Khởi tạo OrderService với các mock để test riêng phần logic
    //    _orderService = new OrderService(_mockUnitOfWork.Object, _mockMapper.Object,I);
    //}

    // Tạo một user giả lập cho các test case
    private ClaimsPrincipal MockUser(string userId)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
        var identity = new ClaimsIdentity(claims, "Test");
        return new ClaimsPrincipal(identity);
    }

    // Trường hợp 1: Giỏ hàng của user là null => không thể đặt hàng
    [Fact]
    public async Task PlaceOrderAsync_ShouldReturnFalse_WhenCartIsNull()
    {
        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart(It.IsAny<string>()))
                       .ReturnsAsync((Cart)null);

        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest());
        Assert.False(result);  // Kỳ vọng trả về false
    }

    // Trường hợp 2: Giỏ hàng có nhưng không có sản phẩm nào
    [Fact]
    public async Task PlaceOrderAsync_ShouldReturnFalse_WhenCartItemsEmpty()
    {
        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("1"))
                       .ReturnsAsync(new Cart { cartItems = new List<CartItem>() });

        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest());
        Assert.False(result);
    }

    // Trường hợp 3: Sản phẩm trong giỏ không tồn tại hoặc số lượng trong kho không đủ
    [Theory]
    [InlineData(null, 1)]  // Sản phẩm không tồn tại (stock null)
    [InlineData(5, 10)]    // Số lượng trong kho (5) không đủ với số lượng đặt (10)
    public async Task PlaceOrderAsync_ShouldReturnFalse_WhenProductInvalid(int? stock, int cartQuantity)
    {
        var cartItems = new List<CartItem>
        {
            new CartItem { Productid = 1, quanlity = cartQuantity, price = 100, CartitemId = 10 }
        };

        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("1"))
                       .ReturnsAsync(new Cart { cartItems = cartItems });

        _mockUnitOfWork.Setup(u => u.OrderRepo.BeginTransactionAsync())
                       .ReturnsAsync(new Mock<IDbContextTransaction>().Object);

        // Nếu stock có giá trị thì trả về product, ngược lại trả về null
        Product product = stock.HasValue ? new Product { stock = stock.Value } : null;
        _mockUnitOfWork.Setup(u => u.ProductRepo.GetProductsByIdsAsync(1))
                       .ReturnsAsync(product);

        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest());
        Assert.False(result); // Kỳ vọng thất bại
    }

    // Trường hợp 4: Lỗi khi xóa sản phẩm trong giỏ hàng (bất thường)
    [Fact]
    public async Task PlaceOrderAsync_ShouldReturnFalse_WhenCartDeletionFails()
    {
        var cartItems = new List<CartItem> { new CartItem { Productid = 1, quanlity = 1, price = 100, CartitemId = 10 } };
        var product = new Product { stock = 5 };

        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("1"))
                       .ReturnsAsync(new Cart { cartItems = cartItems });

        _mockUnitOfWork.Setup(u => u.OrderRepo.BeginTransactionAsync())
                       .ReturnsAsync(new Mock<IDbContextTransaction>().Object);

        _mockUnitOfWork.Setup(u => u.ProductRepo.GetProductsByIdsAsync(1))
                       .ReturnsAsync(product);

        // Giả lập việc xóa giỏ hàng bị lỗi, ném Exception
        _mockUnitOfWork.Setup(u => u.cartRepo.RemoveCartItemsByIdsAsync(It.IsAny<List<int>>()))
                       .Throws(new Exception());

        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest());
        Assert.False(result); // Kỳ vọng thất bại
    }

    // Trường hợp 5: Bắt lỗi chung khi gọi CheckUserCart ném exception
    [Fact]
    public async Task PlaceOrderAsync_ShouldReturnFalse_WhenUnhandledExceptionOccurs()
    {
        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("1"))
                       .Throws(new Exception());

        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest());
        Assert.False(result);
    }

    // Trường hợp 6: Thông tin đặt hàng thiếu (phone hoặc address)
    [Theory]
    [InlineData("", "123 Street")]
    [InlineData("0123456789", "")]
    [InlineData("", "")]
    public async Task PlaceOrderAsync_ShouldReturnFalse_WhenOrderInfoMissing(string phone, string address)
    {
        var cartItems = new List<CartItem>
        {
            new CartItem { Productid = 1, quanlity = 1, price = 100, CartitemId = 10 }
        };

        var product = new Product { stock = 5 };

        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("1"))
                       .ReturnsAsync(new Cart { cartItems = cartItems });

        _mockUnitOfWork.Setup(u => u.OrderRepo.BeginTransactionAsync())
                       .ReturnsAsync(new Mock<IDbContextTransaction>().Object);

        _mockUnitOfWork.Setup(u => u.ProductRepo.GetProductsByIdsAsync(1))
                       .ReturnsAsync(product);

        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest
        {
            PhoneNumber = phone,
            ShippingAddress = address
        });

        Assert.False(result);
    }

    // Trường hợp 7: Một sản phẩm không tìm thấy (trong giỏ nhiều sản phẩm)
    [Fact]
    public async Task PlaceOrderAsync_ShouldReturnFalse_WhenOneProductNotFound()
    {
        var cartItems = new List<CartItem>
        {
            new CartItem { Productid = 1, quanlity = 1, price = 100 },
            new CartItem { Productid = 2, quanlity = 2, price = 200 }
        };

        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("1"))
                       .ReturnsAsync(new Cart { cartItems = cartItems });

        _mockUnitOfWork.Setup(u => u.OrderRepo.BeginTransactionAsync())
                       .ReturnsAsync(new Mock<IDbContextTransaction>().Object);

        // Sản phẩm 1 tồn tại
        _mockUnitOfWork.Setup(u => u.ProductRepo.GetProductsByIdsAsync(1))
                       .ReturnsAsync(new Product { stock = 10 });

        // Sản phẩm 2 không tồn tại
        _mockUnitOfWork.Setup(u => u.ProductRepo.GetProductsByIdsAsync(2))
                       .ReturnsAsync((Product)null);

        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest());
        Assert.False(result);
    }

    // Trường hợp 8: Đặt hàng thành công với dữ liệu hợp lệ
    [Fact]
    public async Task PlaceOrderAsync_ShouldReturnTrue_WhenOrderIsValid()
    {
        var cartItems = new List<CartItem>
        {
            new CartItem { Productid = 1, quanlity = 1, price = 100, CartitemId = 10 }
        };
        var product = new Product { stock = 5 };

        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("1"))
                       .ReturnsAsync(new Cart { cartItems = cartItems });

        var mockTransaction = new Mock<IDbContextTransaction>();
        mockTransaction.Setup(t => t.CommitAsync(It.IsAny<CancellationToken>()))
                       .Returns(Task.CompletedTask);
        mockTransaction.Setup(t => t.RollbackAsync(It.IsAny<CancellationToken>()))
                       .Returns(Task.CompletedTask);

        _mockUnitOfWork.Setup(u => u.OrderRepo.BeginTransactionAsync())
                       .ReturnsAsync(mockTransaction.Object);

        _mockUnitOfWork.Setup(u => u.ProductRepo.GetProductsByIdsAsync(1))
                       .ReturnsAsync(product);

        _mockUnitOfWork.Setup(u => u.cartRepo.RemoveCartItemsByIdsAsync(It.IsAny<List<int>>()))
                       .Returns(Task.CompletedTask);

        _mockUnitOfWork.Setup(u => u.OrderRepo.CreateOrderAsync(It.IsAny<Order>()))
                       .Returns(Task.CompletedTask);

        _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest
        {
            PhoneNumber = "123456789",
            ShippingAddress = "Test Address"
        });

        Assert.True(result); // Kỳ vọng thành công
    }
}