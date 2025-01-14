using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.CartService;

public class PaymentService : BaseService<Payment>, IPaymentService
{
    public PaymentService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    
}