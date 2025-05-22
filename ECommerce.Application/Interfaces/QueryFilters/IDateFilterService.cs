using ECommerce.Application.Features.Orders.Queries.GetOrders;

namespace ECommerce.Application.Interfaces.QueryFilters
{
    public interface IDateFilterService
    {
        (DateTime? StartDate, DateTime? EndDate) DateFilter(OrderFilterType filterType, DateTime? startDate, DateTime? endDate);
    }
}
