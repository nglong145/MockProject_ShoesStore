using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.BrandService
{
    public class BrandService : BaseService<Brand>, IBrandService
    {
        public BrandService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
