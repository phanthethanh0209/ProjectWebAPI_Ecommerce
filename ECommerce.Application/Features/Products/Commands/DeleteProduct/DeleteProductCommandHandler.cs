using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;


namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ResultResponse<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _unitOfWork.Product.GetFirstOrDefaultAsync(t => t.Id == request.Id);
            if (product == null)
            {
                throw new NotFoundException("Product", request.Id);
            }

            await _unitOfWork.Product.Delete(product);
            await _unitOfWork.SaveChangeAsync();

            return ResultResponse<Unit>.SuccessResponse(Unit.Value);

        }
    }
}
