using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.ProductService
{
    public class ProductService:BaseService<Product>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork):base(unitOfWork) {
            
        }

        public async Task<PaginatedResult<Product>> GetProductsByStatusAsync(string status, int pageIndex, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<Product>().GetQuery(p => p.Status == status).Include(p => p.Brand);
            return await PaginatedResult<Product>.CreateAsync(query, pageIndex, pageSize);
        }

        public async Task<PaginatedResult<Product>> GetProductsSimilarAsync(string status,Guid brandId, Guid productId, int pageIndex, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<Product>().GetQuery(p => p.Status == status && p.BrandId==brandId && p.ProductId!= productId).Include(p => p.Brand);
            return await PaginatedResult<Product>.CreateAsync(query, pageIndex, pageSize);
        }


        public async Task<PaginatedResult<Product>> GetFilteredProductsAsync(ProductFilterVm filter)
        {
            var query = _unitOfWork.GenericRepository<Product>().GetQuery().Include(p => p.Brand);

            // Filter by status
            if (!string.IsNullOrEmpty(filter.Status))
            {
                query = query.Where(p => p.Status == filter.Status).Include(p => p.Brand);
            }

            // Filter by brand
            if (filter.BrandId.HasValue)
            {
                query = query.Where(p => p.BrandId == filter.BrandId.Value).Include(p => p.Brand);
            }

            // Filter by size
            if (!string.IsNullOrEmpty(filter.SizeName))
            {
                query = query.Where(p => p.Sizes.Any(s => s.SizeName == filter.SizeName)).Include(p => p.Brand);
            }

            // Filter by price
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value).Include(p => p.Brand);
            }
            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value).Include(p => p.Brand);
            }

            return await PaginatedResult<Product>.CreateAsync(query, filter.PageIndex, filter.PageSize);
        }
    }
}
