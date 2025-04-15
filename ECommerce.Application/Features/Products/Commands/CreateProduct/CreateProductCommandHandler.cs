using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);
            await _unitOfWork.Product.AddAsync(product);
            await _unitOfWork.SaveChangeAsync();

            return ResultResponse<Guid>.SuccessResponse(product.Id);
        }
    }
}
