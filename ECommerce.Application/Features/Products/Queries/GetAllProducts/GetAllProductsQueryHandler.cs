using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Products.DTOs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetAllProductsQuery, ResultResponse<IEnumerable<GetProductResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<IEnumerable<GetProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = await _unitOfWork.Product.GetAllAsync(null, request.PageNumber, request.Limit);
            //if (products.Count() <= 0)
            //{
            //    return ResultResponse<IEnumerable<GetProductResponse>>.FailResponse("Product All not found");
            //}

            if (!products.Any())
                throw new NotFoundException("Product", request.Filter);

            IEnumerable<GetProductResponse> data = _mapper.Map<IEnumerable<GetProductResponse>>(products);
            return ResultResponse<IEnumerable<GetProductResponse>>.SuccessResponse(data);
        }
    }
}
