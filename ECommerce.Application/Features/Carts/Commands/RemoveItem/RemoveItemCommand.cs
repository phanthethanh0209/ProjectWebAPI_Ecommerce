using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Carts.Commands.RemoveItem
{
    public record class RemoveItemCommand(Guid productId) : IRequest<ResultResponse<Guid>>;
}
