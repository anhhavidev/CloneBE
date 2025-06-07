using Xunit;
using Moq;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CloneBE.Application.Interface.Serivce;
using CloneBE.Domain.InterfaceRepo;
using CloneBE.Domain.EF;
using CloneBE.Application.DTO;
using CloneBE.Application.DTO.Request;
using System.Data;
using CloneBE.Application.Helper;
using Microsoft.EntityFrameworkCore.Storage;

public class OrderServiceTests
{
    private readonly Mock<IUnitOfWork1> mockUnitOfWork;
    private readonly Mock<IMapper> mockMapper;
    private readonly Mock<ISendMailService> mockEmailService;
    private readonly OrderService orderService;

    public OrderServiceTests()
    {
        mockUnitOfWork = new Mock<IUnitOfWork1>();
        mockMapper = new Mock<IMapper>();
        mockEmailService = new Mock<ISendMailService>();
        orderService = new OrderService(mockUnitOfWork.Object, mockMapper.Object, mockEmailService.Object);
    }

    // Tạo user giả lập với userId mặc định
    private ClaimsPrincipal CreateMockUser(string userId = "test-user")
    {
        return new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        }));
    }

    [Fact] //✅ Đặt đơn hàng thành công
    public async Task PlaceOrderAsync_Success()
    {
        var user = CreateMockUser();
        var request = new OrderRequest { PhoneNumber = "0123456789", ShippingAddress = "ABC Street" };
        var cart = new Cart
        {
            cartItems = new List<CartItem> {
                new CartItem { Productid = 1, quanlity = 2, price = 100 }
            }
        };

        mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("test-user")).ReturnsAsync(cart);
        mockUnitOfWork.Setup(u => u.ProductRepo.GetProductsByIdsAsync(1)).ReturnsAsync(new Product { stock = 10 });
        mockUnitOfWork.Setup(u => u.OrderRepo.BeginTransactionAsync())
       .ReturnsAsync(Mock.Of<IDbContextTransaction>());

        mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

        var result = await orderService.PlaceOrderAsync(user, request);

        Assert.True(result);
    }

    [Theory] // ❌ Thất bại nếu thiếu số điện thoại hoặc địa chỉ
    [InlineData("", "123 Street")]
    [InlineData("0123456789", "")]
    [InlineData("", "")]
    public async Task PlaceOrderAsync_MissingPhoneOrAddress_Fails(string phone, string address)
    {
        var user = CreateMockUser();
        var request = new OrderRequest { PhoneNumber = phone, ShippingAddress = address };

        var cart = new Cart
        {
            cartItems = new List<CartItem> {
                new CartItem { Productid = 1, quanlity = 1, price = 100 }
            }
        };

        mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("test-user")).ReturnsAsync(cart);

        var result = await orderService.PlaceOrderAsync(user, request);
        Assert.False(result);
    }

    [Fact] // ❌ Thất bại: Không có userId
    public async Task PlaceOrderAsync_NoUserId_Fails()
    {
        var result = await orderService.PlaceOrderAsync(new ClaimsPrincipal(), new OrderRequest());
        Assert.False(result);
    }

    [Fact] // ❌ Thất bại: Giỏ hàng rỗng
    public async Task PlaceOrderAsync_EmptyCart_Fails()
    {
        var user = CreateMockUser();
        mockUnitOfWork.Setup(u => u.cartRepo.CheckUserCart("test-user")).ReturnsAsync(new Cart
        {
            cartItems = new List<CartItem>()
        });

        var result = await orderService.PlaceOrderAsync(user, new OrderRequest { PhoneNumber = "123", ShippingAddress = "xyz" });
        Assert.False(result);
    }

    [Fact] // ✅ Hủy đơn thành công
    public async Task CancelOrderAsync_Success()
    {
        var user = CreateMockUser();
        var order = new Order
        {
            OrderId = 1,
            UserId = "test-user",
            Status = "Pending",
            OrderDetails = new List<OrderDetail> { new OrderDetail { ProductId = 1, Quantity = 1 } }
        };

        mockUnitOfWork.Setup(u => u.OrderRepo.GetOrderByIdAsync(1)).ReturnsAsync(order);
        mockUnitOfWork.Setup(u => u.ProductRepo.GetProductsByIdsAsync(1)).ReturnsAsync(new Product());
        mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        var result = await orderService.CancelOrderAsync(1, user);
        Assert.True(result);
    }

    [Fact] // ❌ Hủy đơn thất bại vì đã duyệt
    public async Task CancelOrderAsync_ApprovedOrder_Fails()
    {
        var user = CreateMockUser();
        var order = new Order { OrderId = 1, UserId = "test-user", Status = "Approved" };

        mockUnitOfWork.Setup(u => u.OrderRepo.GetOrderByIdAsync(1)).ReturnsAsync(order);

        var result = await orderService.CancelOrderAsync(1, user);
        Assert.False(result);
    }

    [Fact] // ✅ Thanh toán đơn thành công
    public async Task PayOrderAsync_Success()
    {
        var user = CreateMockUser();
        var order = new Order { OrderId = 1, UserId = "test-user", Status = "Pending" };

        mockUnitOfWork.Setup(u => u.OrderRepo.GetOrderByIdAsync(1)).ReturnsAsync(order);
        mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        var result = await orderService.PayOrderAsync(1, user);
        Assert.True(result);
    }

    [Fact] // ❌ Thanh toán thất bại vì trạng thái không hợp lệ
    public async Task PayOrderAsync_InvalidStatus_Fails()
    {
        var user = CreateMockUser();
        var order = new Order { OrderId = 1, UserId = "test-user", Status = "Approved" };

        mockUnitOfWork.Setup(u => u.OrderRepo.GetOrderByIdAsync(1)).ReturnsAsync(order);

        var result = await orderService.PayOrderAsync(1, user);
        Assert.False(result);
    }

    [Fact] // ✅ Duyệt đơn thành công
    public async Task ApproveOrderAsync_Success()
    {
        var order = new Order { OrderId = 1, Status = "Paid" };

        mockUnitOfWork.Setup(u => u.OrderRepo.GetOrderByIdAsync(1)).ReturnsAsync(order);
        mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        var result = await orderService.ApproveOrderAsync(1);
        Assert.True(result);
    }

    [Fact] // ❌ Duyệt đơn thất bại nếu không phải trạng thái "Paid"
    public async Task ApproveOrderAsync_InvalidStatus_Fails()
    {
        var order = new Order { OrderId = 1, Status = "Pending" };

        mockUnitOfWork.Setup(u => u.OrderRepo.GetOrderByIdAsync(1)).ReturnsAsync(order);

        var result = await orderService.ApproveOrderAsync(1);
        Assert.False(result);
    }

   [Fact] // ✅ Xoá đơn chưa duyệt thành công
    public async Task DeleteOrderAsync_Success()
    {
        var order = new Order { OrderId = 1, Status = "Pending" };

        mockUnitOfWork.Setup(u => u.OrderRepo.GetOrderByIdAsync(1)).ReturnsAsync(order);
        mockUnitOfWork.Setup(u => u.OrderRepo.DeleteOrderAsync(1)).ReturnsAsync(true); // Sửa ở đây
        mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        var result = await orderService.DeleteOrderAsync(1);
        Assert.True(result);
    }

    [Fact] // ❌ Xoá đơn thất bại nếu đã duyệt
    public async Task DeleteOrderAsync_ApprovedOrder_Fails()
    {
        var order = new Order { OrderId = 1, Status = "Approved" };

        mockUnitOfWork.Setup(u => u.OrderRepo.GetOrderByIdAsync(1)).ReturnsAsync(order);

        var result = await orderService.DeleteOrderAsync(1);
        Assert.False(result);
    }

    //[Fact] // ✅ Lấy tất cả đơn hàng
    //public async Task GetAllOrdersAsync_ReturnsOrders()
    //{
    //    mockUnitOfWork.Setup(u => u.OrderRepo.GetAllOrdersAsync()).ReturnsAsync(new List<Order>());
    //    mockMapper.Setup(m => m.Map<IEnumerable<OrderDTO>>(It.IsAny<IEnumerable<Order>>()))
    //        .Returns(new List<OrderDTO>());

    //    var result = await orderService.GetAllOrdersAsync();
    //    Assert.NotNull(result);
    //}

    //[Fact] // ✅ Lấy đơn theo ID
    //public async Task GetOrderByIdAsync_ReturnsOrder()
    //{
    //    var order = new Order();
    //    mockUnitOfWork.Setup(u => u.OrderRepo.GetOrderByIdAsync(1)).ReturnsAsync(order);
    //    mockMapper.Setup(m => m.Map<OrderDTO>(order)).Returns(new OrderDTO());

    //    var result = await orderService.GetOrderByIdAsync(1);
    //    Assert.NotNull(result);
    //}

    //[Fact] // ✅ Lấy danh sách đơn theo user
    //public async Task GetOrdersByUserAsync_ReturnsOrders()
    //{
    //    var user = CreateMockUser();
    //    mockUnitOfWork.Setup(u => u.OrderRepo.GetOrdersByUserIdAsync("test-user"))
    //        .ReturnsAsync(new List<Order>());
    //    mockMapper.Setup(m => m.Map<IEnumerable<OrderDTO>>(It.IsAny<IEnumerable<Order>>()))
    //        .Returns(new List<OrderDTO>());

    //    var result = await orderService.GetOrdersByUserAsync(user);
    //    Assert.NotNull(result);
    //}
}
