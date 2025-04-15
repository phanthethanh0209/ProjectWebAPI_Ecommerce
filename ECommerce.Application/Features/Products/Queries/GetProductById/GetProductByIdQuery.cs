using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Products.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ResultResponse<GetProductResponse>>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
