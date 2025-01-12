using ShoesStoreApp.DAL.Data;
using ShoesStoreApp.DAL.Models;
using ShoesStoreApp.DAL.Repositories;

namespace ShoesStoreApp.DAL.Infrastructure
{
    public interface IUnitOfWork:IDisposable
    {
        ShoesStoreAppDbContext Context { get; }
        IGenericRepository<Brand> BrandRepository { get; }
        IGenericRepository<Blog> BlogRepository { get; }
        IGenericRepository<ImageSystem> ImageRepository { get; }
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
