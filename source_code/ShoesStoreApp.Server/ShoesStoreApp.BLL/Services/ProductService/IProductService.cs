using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.ProductService
{
    public interface IProductService:IBaseService<Product>
    {
        Task<IEnumerable<Product>> GetProductsByStatusAsync(string status);
    }
}
