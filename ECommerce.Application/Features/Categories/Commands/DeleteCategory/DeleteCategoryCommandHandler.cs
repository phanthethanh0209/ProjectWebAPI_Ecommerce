using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ResultResponse<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultResponse<Unit>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = await _unitOfWork.Category.GetFirstOrDefaultAsync(t => t.Id == request.Id);
            if (category == null)
            {
                throw new NotFoundException("Category", request.Id);
            }
            await _unitOfWork.Category.Delete(category);
            await _unitOfWork.SaveChangesAsync();

            return ResultResponse<Unit>.SuccessResponse(Unit.Value);
        }
    }
}
