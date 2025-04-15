using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<ResultResponse<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
