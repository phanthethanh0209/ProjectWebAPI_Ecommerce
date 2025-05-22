using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Orders.DTOs;
using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Application.Features.Orders.Queries.GetOrders
{
    public record class GetOrdersQuery(int pageNumber = 1, int pageSize = 5, OrderStatus? status = null,
        DateTime? startDate = null, DateTime? endDate = null, OrderFilterType filterType = OrderFilterType.None)
        : IRequest<ResultResponse<PagedList<OrderDTO>>>;

    public enum OrderFilterType
    {
        None,
        LastWeek,
        LastMonth,
        Last3Month,
    }
}
