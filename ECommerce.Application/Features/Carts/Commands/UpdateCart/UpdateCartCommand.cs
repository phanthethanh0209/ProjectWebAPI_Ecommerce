using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Carts.Commands.UpdateCart
{
    public record class UpdateCartCommand(Guid productId, int quantity) : IRequest<ResultResponse<Guid>>;
}
