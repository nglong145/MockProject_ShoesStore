using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.ProductService
{
    public interface IProductService:IBaseService<Product>
    {
        Task<PaginatedResult<Product>> GetProductsByStatusAsync(string status, int pageIndex, int pageSize);
        Task<PaginatedResult<Product>> GetFilteredProductsAsync(ProductFilterVm filter);
    }
}
