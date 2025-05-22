using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Categories.DTOs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace ECommerce.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, ResultResponse<PagedList<GetCategoryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<PagedList<GetCategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Category, bool>>? filter = null;
            if (request.filter != null)
            {
                filter = t => t.Name.Contains(request.filter) || t.Description.Contains(request.filter);
            }

            IEnumerable<Category> categories = await _unitOfWork.Category.GetAllAsync(filter);

            if (categories is null)
            {
                throw new NotFoundException("Category", request.filter);
            }

            IEnumerable<GetCategoryResponse> data = _mapper.Map<IEnumerable<GetCategoryResponse>>(categories);
            PagedList<GetCategoryResponse> result = PagedList<GetCategoryResponse>.CreateAsync(data, request.pageNumber, request.pageSize);
            return ResultResponse<PagedList<GetCategoryResponse>>.SuccessResponse(result);
        }
    }
}
