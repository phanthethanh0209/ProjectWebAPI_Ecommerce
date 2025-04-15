using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
        public Task Update(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter, int pageNumber, int limit)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.Skip((pageNumber - 1) * limit).Take(limit);
            return await query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>>? filter)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.AnyAsync();
        }
    }
}
