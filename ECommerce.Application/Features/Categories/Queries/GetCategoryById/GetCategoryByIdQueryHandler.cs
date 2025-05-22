using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Categories.DTOs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ResultResponse<GetCategoryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<GetCategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            Category category = await _unitOfWork.Category.GetFirstOrDefaultAsync(t => t.Id == request.id);
            if (category == null)
                throw new NotFoundException(nameof(category), request.id);

            GetCategoryResponse data = _mapper.Map<GetCategoryResponse>(category);
            return ResultResponse<GetCategoryResponse>.SuccessResponse(data);
        }
    }
}
