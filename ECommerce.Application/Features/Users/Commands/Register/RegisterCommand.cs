using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Users.Commands.Register
{
    public record class RegisterCommand(string name, string email, string password, string phone)
        : IRequest<ResultResponse<Guid>>;
}
