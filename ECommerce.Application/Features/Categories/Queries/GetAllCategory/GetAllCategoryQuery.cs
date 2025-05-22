using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Categories.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetAllCategory
{
    public record class GetAllCategoryQuery(int pageNumber = 1, int pageSize = 5, string? filter = null)
        : IRequest<ResultResponse<PagedList<GetCategoryResponse>>>;
}
