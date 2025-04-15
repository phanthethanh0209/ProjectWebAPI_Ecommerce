using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = _mapper.Map<Category>(request);
            await _unitOfWork.Category.AddAsync(category);
            await _unitOfWork.SaveChangeAsync();

            return ResultResponse<Guid>.SuccessResponse(category.Id);
        }
    }
}
