using System.Linq.Expressions;

namespace ECommerce.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter, params Expression<Func<T, object>>[]? includes);
        Task<bool> AnyAsync(Expression<Func<T, bool>>? filter);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);

    }
}
