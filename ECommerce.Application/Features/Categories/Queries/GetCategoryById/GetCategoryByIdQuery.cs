using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Categories.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryById
{
    public record class GetCategoryByIdQuery(Guid id)
        : IRequest<ResultResponse<GetCategoryResponse>>;
}
