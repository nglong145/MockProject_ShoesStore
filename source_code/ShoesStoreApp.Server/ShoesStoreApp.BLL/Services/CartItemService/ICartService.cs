using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.CartService;

public interface ICartItemService : IBaseService<CartItem>
{
    Task<CartItem> GetCartItemAsync(Guid cartId, Guid productId, string Size);
}