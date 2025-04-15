using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Authentication.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.Refresh
{
    public record class RefreshTokenCommand(string refreshToken) : IRequest<ResultResponse<LoginResponse>>;
}
