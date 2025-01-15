using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.CartService;
using ShoesStoreApp.BLL.Services.Custumer;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.PLA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService;
    private readonly IUserService _userService;
    private readonly ICartItemService _cartItemService;
    private readonly IOrderItemService _orderItemService;

    public OrderController(
        IOrderService orderService, 
        ICartService cartService,
        ICartItemService cartItemService,
        IUserService userService, 
        IOrderItemService orderItemService)
    {
        _orderService = orderService;
        _cartService = cartService;
        _cartItemService = cartItemService;
        _userService = userService;
        _orderItemService = orderItemService;
    }
    
    [HttpPost("add-new-order")]
    public async Task<IActionResult> AddNewOrder([FromBody] AddOrderVM addOrderVM)
    {
        if (User.Identity.Name == null) return Unauthorized();
        
        var user = await  _userService.getUserByEmail(User.Identity.Name);
        var cartUser = await _cartService.GetCartByUserId(user.Id);
        decimal totalPrice = 0;
        
        // add order
        var order = new Order()
        {
            UserId = user.Id,
            PaymentId = addOrderVM.PaymentId,
            ReceiverName = addOrderVM.ReceiverName,
            ReceiverAddress = addOrderVM.ReceiverAddress,
            ReceiverPhone = addOrderVM.ReceiverPhone,
            IsPayment = false,
            Status = "Pending",
        };
        await _orderService.AddAsync(order);
        
        // Add item in order
        foreach (var item in cartUser.Items)
        {
            // add order item
            var orderItem = new OrderItem()
            {
                OrderId = order.OrderId,
                ProductId = item.ProductId,
                Price = item.Price,
                Quantity = item.Quantity,
            };
            await _orderItemService.AddAsync(orderItem);
            
            // calculate total price
            var price = orderItem.Price * item.Quantity;
            totalPrice += price;
            
            // Delete item in cart
            var cartItem = await _cartItemService.GetCartItemAsync(item.CartId, item.ProductId);
            await _cartItemService.DeleteAsync(cartItem);
        }
        
        order.Total = totalPrice;
        await _orderService.UpdateAsync(order);

        return Ok(order.OrderId);
    }
}