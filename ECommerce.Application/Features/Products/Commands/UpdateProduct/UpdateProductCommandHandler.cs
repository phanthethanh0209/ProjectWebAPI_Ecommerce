using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;


namespace ECommerce.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _unitOfWork.Product.GetFirstOrDefaultAsync(t => t.Id == request.Id);
            if (product == null)
            {
                throw new NotFoundException("Product", request.Id);
            }

            bool categoryExists = await _unitOfWork.Category.AnyAsync(t => t.Id == request.CategoryId);
            if (!categoryExists)
            {
                throw new NotFoundException("Category", request.CategoryId);
            }

            _mapper.Map(request, product); // map data from command to product
            await _unitOfWork.Product.Update(product);
            await _unitOfWork.SaveChangeAsync();

            return ResultResponse<Guid>.SuccessResponse(product.Id);
        }
    }
}
