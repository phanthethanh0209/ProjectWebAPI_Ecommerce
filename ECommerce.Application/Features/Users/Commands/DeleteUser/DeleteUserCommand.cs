using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Users.Commands.DeleteUser
{
    public record class DeleteUserCommand(Guid Id) : IRequest<ResultResponse<Unit>>;
}
