using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Orders.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Orders.Queries.GetOrderById
{
    public record class GetOrderByIdQuery(Guid orderId) : IRequest<ResultResponse<OrderDTO>>;
}
