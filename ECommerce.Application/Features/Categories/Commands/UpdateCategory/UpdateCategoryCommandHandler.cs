using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ResultResponse<Guid>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = await _unitOfWork.Category.GetFirstOrDefaultAsync(t => t.Id == request.Id);
            if (category == null)
            {
                throw new NotFoundException("Category", request.Id);
            }

            await _unitOfWork.Category.Update(category);
            await _unitOfWork.SaveChangeAsync();

            return ResultResponse<Guid>.SuccessResponse(category.Id);
        }
    }
}
