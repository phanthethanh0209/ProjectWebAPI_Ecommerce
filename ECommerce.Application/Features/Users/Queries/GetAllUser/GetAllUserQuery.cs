using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Users.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetAllUser
{
    public record class GetAllUserQuery(int pageNummber, int limit, string? filter = null)
        : IRequest<ResultResponse<IEnumerable<GetUserResponse>>>;
}
