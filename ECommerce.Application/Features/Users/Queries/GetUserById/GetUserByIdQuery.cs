using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Users.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetUserById
{
    public record class GetUserByIdQuery(Guid Id) : IRequest<ResultResponse<GetUserResponse>>;
}
