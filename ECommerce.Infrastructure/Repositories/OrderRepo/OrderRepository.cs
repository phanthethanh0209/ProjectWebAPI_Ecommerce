using ECommerce.Application.Interfaces.Repositories.OrderRepository;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Repositories.OrderRepo
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext db) : base(db) { }


        public async Task<Order?> GetOrderByIdWithItemsAsync(Guid orderId)
        {
            return await _db.Orders
                .Include(t => t.OrderItems)
                .FirstOrDefaultAsync(t => t.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId, int pageNumber = 1, int limit = 5, Expression<Func<Order, bool>>? filter = null)
        {
            IQueryable<Order> query = _db.Orders.Where(t => t.UserId == userId);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.OrderByDescending(t => t.CreatedAt);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int pageNumber = 1, int limit = 5,
            Expression<Func<Order, bool>>? filter = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            IQueryable<Order> query = _db.Orders.Include(t => t.OrderItems);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (startDate.HasValue)
            {
                query = query.Where(t => t.CreatedAt >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.CreatedAt <= endDate.Value);
            }

            query = query.OrderByDescending(t => t.CreatedAt);

            return await query.ToListAsync();
        }
    }
}
