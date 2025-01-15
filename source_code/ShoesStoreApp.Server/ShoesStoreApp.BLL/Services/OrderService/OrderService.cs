using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.CartService;

public class OrderService : BaseService<Order>, IOrderService
{
    public OrderService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    
}