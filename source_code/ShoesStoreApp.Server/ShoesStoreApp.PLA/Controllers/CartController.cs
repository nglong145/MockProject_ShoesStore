using System.Collections;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.CartService;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.PLA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("get-cart-by-userId/{userId}")]
    public async Task<IActionResult> GetCartByUserId(Guid userId)
    {
        var cart = await _cartService.GetCartByUserId(userId);
        
        if (cart == null)
            return BadRequest("No cart found");
        
        return Ok(cart);
    }
}