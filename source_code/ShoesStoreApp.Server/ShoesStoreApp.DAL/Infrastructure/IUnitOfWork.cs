using ShoesStoreApp.DAL.Data;
using ShoesStoreApp.DAL.Repositories;

namespace ShoesStoreApp.DAL.Infrastructure
{
    public interface IUnitOfWork:IDisposable
    {
        ShoesStoreAppDbContext Context { get; }
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
