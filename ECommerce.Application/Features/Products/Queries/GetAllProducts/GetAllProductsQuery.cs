using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Products.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<ResultResponse<IEnumerable<GetProductResponse>>>
    {
        public string? Filter { get; set; }
        public int PageNumber { get; set; }
        public int Limit { get; set; }

        public GetAllProductsQuery(int pageNumber = 1, int limit = 5, string? filter = null)
        {
            PageNumber = pageNumber;
            Limit = limit;
            Filter = filter;
        }
    }
}
