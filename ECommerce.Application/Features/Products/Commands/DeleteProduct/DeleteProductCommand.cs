using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    public class DeleteProductCommand : IRequest<ResultResponse<Unit>>
    {

        public Guid Id { get; set; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
