using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.CartService;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.PLA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartItemController : ControllerBase
{
    private readonly ICartItemService _cartItemService;

    public CartItemController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    [HttpPost("add-cart-item")]
    public async Task<IActionResult> AddCartItem([FromBody] AddCartItem cartItem)
    {
        // var product = await .GetByIdAsync(cartItem.ProductId);
        // if (product == null)
        //     return BadRequest("Product not found");
    
        var item = new CartItem()
        {
            CartId = cartItem.CartId,
            ProductId = cartItem.ProductId,
            Price = cartItem.Price,
            Quantity = cartItem.Quantity
        };
        
        await _cartItemService.AddAsync(item);
        return Ok(item);
    }

    [HttpPut("update-cart-item/{cartId}/{productId}")]
    public async Task<IActionResult> UpdateCartItem(Guid cartId, Guid productId, [FromBody] UpdateCartItemVM updateCartItem)
    {
        var cartItem = await _cartItemService.GetCartItemAsync(cartId, productId);
        if (cartItem == null)
            return NotFound("cartItem not found");
        
        cartItem.Quantity = updateCartItem.Quantity;
        cartItem.Price = updateCartItem.Price;
        
        await _cartItemService.UpdateAsync(cartItem);
        return Ok(cartItem);
    }

    [HttpDelete("remove-cart-item/{cartId}/{productId}")]
    public async Task<IActionResult> DeleteCartItem(Guid cartId, Guid productId)
    {
        var cartItem = await _cartItemService.GetCartItemAsync(cartId, productId);
        if (cartItem == null)
            return NotFound("cartItem not found");
        
        await _cartItemService.DeleteAsync(cartItem);
        return Ok(cartItem);
    }
}