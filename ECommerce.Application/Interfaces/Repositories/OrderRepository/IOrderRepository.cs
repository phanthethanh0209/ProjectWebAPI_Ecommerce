using ECommerce.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerce.Application.Interfaces.Repositories.OrderRepository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> GetOrderByIdWithItemsAsync(Guid orderId);
        Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId, int pageNumber = 1, int limit = 5, Expression<Func<Order, bool>>? filter = null);
        Task<IEnumerable<Order>> GetOrdersAsync(int pageNumber = 1, int limit = 5, Expression<Func<Order, bool>>? filter = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
