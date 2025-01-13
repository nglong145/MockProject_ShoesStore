using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesStoreApp.BLL.Services.ProductService
{
    public class ProductService:BaseService<Product>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork):base(unitOfWork) {
            
        }

        public async Task<IEnumerable<Product>> GetProductsByStatusAsync(string status)
        {
            return await _unitOfWork.GenericRepository<Product>()
                                    .GetQuery(p => p.Status == status)
                                    .ToListAsync();
        }
    }
}
