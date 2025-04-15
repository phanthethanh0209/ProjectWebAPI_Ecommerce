using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Categories.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQuery : IRequest<ResultResponse<IEnumerable<GetCategoryResponse>>>
    {
        public string? Filter { get; set; }
        public int PageNumber { get; set; }
        public int Limit { get; set; }

        public GetAllCategoryQuery(int pageNumber = 1, int limit = 5, string? filter = null)
        {
            PageNumber = pageNumber;
            Limit = limit;
            Filter = filter;
        }
    }
}
