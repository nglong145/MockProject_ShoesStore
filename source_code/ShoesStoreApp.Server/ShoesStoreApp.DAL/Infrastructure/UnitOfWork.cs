using ShoesStoreApp.DAL.Data;
using ShoesStoreApp.DAL.Models;
using ShoesStoreApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesStoreApp.DAL.Infrastructure
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ShoesStoreAppDbContext _context;
        public ShoesStoreAppDbContext Context => _context;

        private IGenericRepository<Brand>? _brandRepository;
        private IGenericRepository<Blog>? _blogRepository;
        private IGenericRepository<ImageSystem>? _imageRepository;

        public IGenericRepository<Brand> BrandRepository => _brandRepository ?? new GenericRepository<Brand>(_context);
        public IGenericRepository<Blog> BlogRepository => _blogRepository ?? new GenericRepository<Blog>(_context);
        public IGenericRepository<ImageSystem> ImageRepository => _imageRepository ?? new GenericRepository<ImageSystem>(_context);
        public UnitOfWork(ShoesStoreAppDbContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_context);
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
