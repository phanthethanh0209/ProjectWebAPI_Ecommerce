using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<ResultResponse<Unit>>
    {
        public Guid Id { get; set; }

        public DeleteCategoryCommand(Guid id)
        {
            Id = id;
        }
    }
}
