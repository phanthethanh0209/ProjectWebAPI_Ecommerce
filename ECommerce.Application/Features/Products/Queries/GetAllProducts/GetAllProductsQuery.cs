using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Products.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts
{
    public record class GetAllProductsQuery(int pageNumber = 1, int pageSize = 5, string? filter = null)
        : IRequest<ResultResponse<PagedList<GetProductResponse>>>;
}
