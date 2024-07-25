using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;
using Talabat.Repository.Specifications;

namespace Talabat.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _context.products.Include(p => p.Brand).Include(p => p.Category).ToListAsync();
            }

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {

            if (typeof(T) == typeof(Product))
            {
                return await _context.products.Where(p => p.Id == id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync() as T;
            }

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }

        public async Task AddAsync(T item) => await _context.Set<T>().AddAsync(item);

        public void Delete(T item) => _context.Set<T>().Remove(item);

        public void Update(T item) => _context.Set<T>().Update(item);
    }
}
