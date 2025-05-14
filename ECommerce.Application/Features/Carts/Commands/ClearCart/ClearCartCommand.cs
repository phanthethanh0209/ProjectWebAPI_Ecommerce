using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Carts.Commands.ClearCart
{
    public record class ClearCartCommand() : IRequest<ResultResponse<Unit>>;
}
