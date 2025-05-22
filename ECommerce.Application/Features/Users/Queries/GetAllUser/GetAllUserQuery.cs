using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Users.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetAllUser
{
    public record class GetAllUserQuery(int pageNumber, int pageSize, string? filter = null)
        : IRequest<ResultResponse<PagedList<GetUserResponse>>>;
}
