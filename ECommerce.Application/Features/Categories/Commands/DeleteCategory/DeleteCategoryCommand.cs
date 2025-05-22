using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.DeleteCategory
{
    public record class DeleteCategoryCommand(Guid id) : IRequest<ResultResponse<Unit>>;
}
