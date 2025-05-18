
using Xunit;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using CloneBE.Application.Interface.Serivce;
using CloneBE.Application.DTO.Request;
using CloneBE.Domain.InterfaceRepo;
using CloneBE.Domain.EF;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage; // 👈 cần thiết cho IDbContextTransaction

public class OrderServiceTests
{
    private readonly Mock<IUnitOfWork1> _mockUnitOfWork = new();
    private readonly Mock<IMapper> _mockMapper = new();

    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _orderService = new OrderService(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    private ClaimsPrincipal MockUser(string userId)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
        var identity = new ClaimsIdentity(claims, "Test");
        return new ClaimsPrincipal(identity);
    }

    [Fact]
    public async Task PlaceOrderAsync_ShouldReturnFalse_WhenCartIsNull()
    {
        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart(It.IsAny<string>())).ReturnsAsync((Cart)null);
        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest());
        Assert.False(result);
    }

    [Fact]
    public async Task PlaceOrderAsync_ShouldReturnFalse_WhenCartItemsEmpty()
    {
        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("1")).ReturnsAsync(new Cart { cartItems = new List<CartItem>() });
        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest());
        Assert.False(result);
    }

    [Fact]
    public async Task PlaceOrderAsync_ShouldReturnFalse_WhenProductNotExist()
    {
        var cartItems = new List<CartItem> { new CartItem { Productid = 1, quanlity = 1, price = 100, CartitemId = 10 } };
        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("1")).ReturnsAsync(new Cart { cartItems = cartItems });
        _mockUnitOfWork.Setup(u => u.OrderRepo.BeginTransactionAsync())
                 .ReturnsAsync(new Mock<IDbContextTransaction>().Object);

        _mockUnitOfWork.Setup(u => u.ProductRepo.GetProductsByIdsAsync(1)).ReturnsAsync((Product)null);
        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest());
        Assert.False(result);
    }

    [Fact]
    public async Task PlaceOrderAsync_ShouldReturnFalse_WhenStockIsInsufficient()
    {
        var cartItems = new List<CartItem> { new CartItem { Productid = 1, quanlity = 10, price = 100, CartitemId = 10 } };
        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("1")).ReturnsAsync(new Cart { cartItems = cartItems });
        _mockUnitOfWork.Setup(u => u.OrderRepo.BeginTransactionAsync())
                 .ReturnsAsync(new Mock<IDbContextTransaction>().Object);

        _mockUnitOfWork.Setup(u => u.ProductRepo.GetProductsByIdsAsync(1)).ReturnsAsync(new Product { stock = 5 });
        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest());
        Assert.False(result);
    }

    [Fact]
    public async Task PlaceOrderAsync_ShouldReturnTrue_WhenOrderIsValid()
    {
        var cartItems = new List<CartItem> { new CartItem { Productid = 1, quanlity = 1, price = 100, CartitemId = 10 } };
        var product = new Product { stock = 5 };

        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("1")).ReturnsAsync(new Cart { cartItems = cartItems });
        _mockUnitOfWork.Setup(u => u.OrderRepo.BeginTransactionAsync())
                  .ReturnsAsync(new Mock<IDbContextTransaction>().Object);

        _mockUnitOfWork.Setup(u => u.ProductRepo.GetProductsByIdsAsync(1)).ReturnsAsync(product);
        _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).Returns((Task<int>)Task.CompletedTask);
        _mockUnitOfWork.Setup(u => u.cartRepo.RemoveCartItemsByIdsAsync(It.IsAny<List<int>>())).Returns(Task.CompletedTask);

        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest
        {
            PhoneNumber = "123456789",
            ShippingAddress = "Test Address"
        });

        Assert.True(result);
    }

    [Fact]
    public async Task PlaceOrderAsync_ShouldReturnFalse_WhenCartDeletionFails()
    {
        var cartItems = new List<CartItem> { new CartItem { Productid = 1, quanlity = 1, price = 100, CartitemId = 10 } };
        var product = new Product { stock = 5 };

        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("1")).ReturnsAsync(new Cart { cartItems = cartItems });
        _mockUnitOfWork.Setup(u => u.OrderRepo.BeginTransactionAsync())
                   .ReturnsAsync(new Mock<IDbContextTransaction>().Object);

        _mockUnitOfWork.Setup(u => u.ProductRepo.GetProductsByIdsAsync(1)).ReturnsAsync(product);
        _mockUnitOfWork.Setup(u => u.cartRepo.RemoveCartItemsByIdsAsync(It.IsAny<List<int>>())).Throws(new Exception());

        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest());
        Assert.False(result);
    }

    [Fact]
    public async Task PlaceOrderAsync_ShouldReturnFalse_WhenUnhandledExceptionOccurs()
    {
        _mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("1")).Throws(new Exception());
        var result = await _orderService.PlaceOrderAsync(MockUser("1"), new OrderRequest());
        Assert.False(result);
    }
}
