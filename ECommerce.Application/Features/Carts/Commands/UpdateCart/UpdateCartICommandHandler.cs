using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Authentication;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Carts.Commands.UpdateCart
{
    public class UpdateCartICommandHandler : IRequestHandler<UpdateCartCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCartICommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<ResultResponse<Guid>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            Guid userId = _currentUserService.GetUserIdForClaims();

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // check if the product and cart are available
                Product? product = await _unitOfWork.Product.GetFirstOrDefaultAsync(t => t.Id == request.productId);
                if (product == null)
                    throw new Exception($"Product with ID {request.productId} not found");

                Cart? cart = await _unitOfWork.Carts.GetCartByUserIdWithItemsAsync(userId);
                if (cart == null)
                {
                    throw new Exception("Cart not found");
                }

                // check product with stock quantity in cart
                CartItem? cartItem = await _unitOfWork.Carts.GetCartItemByProductIdAsync(cart.Id, request.productId);
                if (cartItem == null)
                    throw new Exception("Product not found in cart");

                if (product.StockQuantity < request.quantity)
                {
                    throw new Exception($"Not enough stock for product {product.Id}");
                }

                _mapper.Map(request, cartItem);
                // update total cart
                cart.TotalAmount = cart.CartItems.Sum(p => p.Quantity * p.Product.Price);
                await _unitOfWork.Carts.Update(cart);
                await _unitOfWork.CartItem.Update(cartItem);

                await _unitOfWork.CommitTransactionAsync();

                return ResultResponse<Guid>.SuccessResponse(cart.Id);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
