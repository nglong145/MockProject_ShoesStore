using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.BlogService
{
    public class BlogService : BaseService<Blog>, IBlogService
    {
        public BlogService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
