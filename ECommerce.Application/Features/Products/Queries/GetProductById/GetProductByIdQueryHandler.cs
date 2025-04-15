using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Products.DTOs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ResultResponse<GetProductResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<GetProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product product = await _unitOfWork.Product.GetFirstOrDefaultAsync(t => t.Id == request.Id);
            //if (product == null)
            //{
            //    return ResultResponse<GetProductResponse>.FailResponse("Product not found");
            //}

            if (product is null)
                throw new NotFoundException("Product", request.Id);

            GetProductResponse data = _mapper.Map<GetProductResponse>(product);
            return ResultResponse<GetProductResponse>.SuccessResponse(data);
        }
    }
}
