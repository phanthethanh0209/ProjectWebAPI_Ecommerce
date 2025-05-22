using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Orders.DTOs;
using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Application.Features.Orders.Queries.GetUserOrderHistory
{
    public record class GetUserOrderHistoryQuery(int pageNumber = 1, int pageSize = 5, OrderStatus? status = null)
        : IRequest<ResultResponse<PagedList<OrderDTO>>>;
}
