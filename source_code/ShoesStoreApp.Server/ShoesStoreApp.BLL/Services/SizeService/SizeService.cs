using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.SizeService
{
    public class SizeService : BaseService<Size>, ISizeSrevice
    {
        public SizeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
