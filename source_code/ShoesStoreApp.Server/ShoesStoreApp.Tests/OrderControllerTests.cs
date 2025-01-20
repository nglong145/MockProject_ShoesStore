using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShoesStoreApp.BLL.Services.CartService;
using ShoesStoreApp.BLL.Services.Custumer;
using ShoesStoreApp.BLL.Services.PaymentService;
using ShoesStoreApp.BLL.Services.SizeService;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.BLL.ViewModels.Payment;
using ShoesStoreApp.DAL.Data;
using ShoesStoreApp.DAL.Models;
using ShoesStoreApp.PLA.Controllers;
using Xunit;

namespace ShoesStoreApp.Tests;
 
public class OrderControllerTests
{
    private readonly Mock<IOrderService> _orderServiceMock;
    private readonly Mock<ICartService> _cartServiceMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<ICartItemService> _cartItemServiceMock;
    private readonly Mock<IOrderItemService> _orderItemServiceMock;
    private readonly Mock<IPaymentService> _paymentServiceMock;
    private readonly Mock<ISizeSrevice> _sizeServiceMock;
    private readonly ShoesStoreAppDbContext _contextMock;
    private readonly OrderController _controller;
    
    public OrderControllerTests()
    {
        _orderServiceMock = new Mock<IOrderService>();
        _cartServiceMock = new Mock<ICartService>();
        _userServiceMock = new Mock<IUserService>();
        _cartItemServiceMock = new Mock<ICartItemService>();
        _orderItemServiceMock = new Mock<IOrderItemService>();
        _paymentServiceMock = new Mock<IPaymentService>();
        _sizeServiceMock = new Mock<ISizeSrevice>();
     
        var options = new DbContextOptionsBuilder<ShoesStoreAppDbContext>()
            .UseSqlServer("Server=LAPTOP-82GOTJUD\\SQLEXPRESS;Database=ShoesStoreApp_DB;Trusted_Connection=True;TrustServerCertificate=True")
            .Options;
        
        _contextMock = new ShoesStoreAppDbContext(options);
        _controller = new OrderController(
            _orderServiceMock.Object,
            _cartServiceMock.Object,
            _cartItemServiceMock.Object,
            _userServiceMock.Object,
            _orderItemServiceMock.Object,
            _paymentServiceMock.Object,
            _sizeServiceMock.Object,
            _contextMock
        );

        // Giả lập HttpContext
        _controller.ControllerContext.HttpContext = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "testuser@example.com"),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            }))
        };
    }
    
    [Fact]
    public async Task AddNewOrder_ShouldReturnUnauthorized_WhenUserNotLoggedIn()
    {
        // Arrange
        _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal();
    
        // Act
        var result = await _controller.AddNewOrder(new AddOrderVM());
    
        // Assert
        Assert.IsAssignableFrom<UnauthorizedResult>(result);
    }
    
    [Fact]
    public async Task AddNewOrder_ShouldReturnOk_WithPaymentUrl_WhenSuccessful()
    {
        // Arrange
        var user = new UserVM()
        {
            Id = new Guid(),
            FullName = "Linh",
            Address = "TN",
            Email = "testuser@example.com",
        };
        
        var cart = new CartVM
        {
            CartId = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            Items = new List<CartItemVM>
            {
                new CartItemVM
                {
                    CartId = Guid.NewGuid(),
                    ProductId = Guid.NewGuid(),
                    ProductName = "Sneaker XYZ",
                    ProductImage = "image_sneaker_xyz.jpg",
                    Size = "M",
                    Price = 100m,
                    Quantity = 2
                }
            }
        };

        var size = new Size() { };
      
        var paymentResponse = new MomoCreatePaymentResponseModel { PayUrl = "https://payment-url.com" };
    
        _userServiceMock.Setup(s => s.getUserByEmail(It.IsAny<string>())).ReturnsAsync(user);
        _cartServiceMock.Setup(s => s.GetCartByUserId(user.Id)).ReturnsAsync(cart);
        _paymentServiceMock.Setup(s => s.CreatePaymentAsync(It.IsAny<Order>())).ReturnsAsync(paymentResponse);
        _sizeServiceMock.Setup(s => s.GetSizesByProductId(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(size); // sizes là đối tượng bạn muốn trả về
        // Act
        var result = await _controller.AddNewOrder(new AddOrderVM
        {
            ReceiverName = "John Doe",
            ReceiverAddress = "123 Street",
            ReceiverPhone = "0123456789",
            PaymentId = Guid.NewGuid()
        }) as OkObjectResult;
    
        // Assert
        Assert.NotNull(result);
        
        Assert.AreEqual(200, result.StatusCode);
        var response = result.Value as MomoCreatePaymentResponseModel;
        Assert.AreEqual("https://payment-url.com", response.PayUrl);
    }
    
    [Fact]
    public async Task AddNewOrder_ShouldReturnInternalServerError_WhenExceptionOccurs()
    {
        var user = new UserVM()
        {
            Id = new Guid(),
            FullName = "Linh",
            Address = "TN",
            Email = "testuser@example.com",
        };
        // Arrange
        _userServiceMock.Setup(s => s.getUserByEmail(It.IsAny<string>())).ReturnsAsync(user);
    
        // Act
        var result = await _controller.AddNewOrder(new AddOrderVM()) as ObjectResult;
    
        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(500, result.StatusCode);
    }

}