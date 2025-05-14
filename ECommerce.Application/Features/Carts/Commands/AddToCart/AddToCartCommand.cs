using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Carts.Commands.AddToCart
{
    public record class AddToCartCommand(Guid productId, int quantity) : IRequest<ResultResponse<Guid>>;
}
