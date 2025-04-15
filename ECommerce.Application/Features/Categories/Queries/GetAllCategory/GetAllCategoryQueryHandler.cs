using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Categories.DTOs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, ResultResponse<IEnumerable<GetCategoryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<IEnumerable<GetCategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Category> categories = await _unitOfWork.Category.GetAllAsync(null, request.PageNumber, request.Limit);
            if (categories is null)
            {
                throw new NotFoundException("Category", request.Filter);
            }
            IEnumerable<GetCategoryResponse> data = _mapper.Map<IEnumerable<GetCategoryResponse>>(categories);
            return ResultResponse<IEnumerable<GetCategoryResponse>>.SuccessResponse(data);
        }
    }
}
