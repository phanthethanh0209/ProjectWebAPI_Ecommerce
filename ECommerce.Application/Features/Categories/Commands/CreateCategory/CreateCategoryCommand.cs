using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<ResultResponse<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
