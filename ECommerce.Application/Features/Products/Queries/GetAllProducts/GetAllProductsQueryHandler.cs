using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Products.DTOs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetAllProductsQuery, ResultResponse<PagedList<GetProductResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<PagedList<GetProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Product, bool>>? filter = null;
            if (request.filter != null)
            {
                filter = t => t.Name.Contains(request.filter) || t.Description.Contains(request.filter)
                            || t.Category.Name.Contains(request.filter)
                            || t.CategoryId.ToString().Equals(request.filter);
            }

            IEnumerable<Product> products = await _unitOfWork.Product.GetAllAsync(filter);

            if (!products.Any())
                throw new NotFoundException("Product", request.filter);

            IEnumerable<GetProductResponse> data = _mapper.Map<IEnumerable<GetProductResponse>>(products);
            PagedList<GetProductResponse> result = PagedList<GetProductResponse>.CreateAsync(data, request.pageNumber, request.pageSize);

            return ResultResponse<PagedList<GetProductResponse>>.SuccessResponse(result);
        }
    }
}
