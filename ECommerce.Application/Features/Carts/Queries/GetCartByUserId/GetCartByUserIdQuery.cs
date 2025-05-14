using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Carts.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Carts.Queries.GetCartByUserId
{
    public record class GetCartByUserIdQuery : IRequest<ResultResponse<CartDTO>>;
}
