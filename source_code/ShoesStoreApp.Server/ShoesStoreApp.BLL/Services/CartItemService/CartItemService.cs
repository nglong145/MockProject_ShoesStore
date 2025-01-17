using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.CartService;

public class CartItemService : BaseService<CartItem>, ICartItemService
{
    public CartItemService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    
    public Task<CartItem> GetCartItemAsync(Guid cartId, Guid productId, string Size)
    {
        var cartItem = _unitOfWork.GenericRepository<CartItem>()
            .GetQuery()
            .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.CartId == cartId && ci.Size == Size);
        return cartItem;
    }
}