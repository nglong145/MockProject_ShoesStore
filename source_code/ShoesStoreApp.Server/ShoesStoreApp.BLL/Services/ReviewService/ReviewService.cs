using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesStoreApp.BLL.Services.ReviewService
{
    public class ReviewService:BaseService<Review>,IReviewService
    {
        public ReviewService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(Guid productId)
        {
            var query = _unitOfWork.GenericRepository<Review>().GetQuery(r => r.ProductId == productId);
            return await query.ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(Guid productId,Guid userId)
        {
            var query = _unitOfWork.GenericRepository<Review>().GetQuery(r => r.ProductId == productId && r.UserId==userId);
            return await query.FirstOrDefaultAsync();
        }


    }
}
