using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Authentication;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Carts.Commands.RemoveItem
{
    public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public RemoveItemCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<ResultResponse<Guid>> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
        {
            Guid userId = _currentUserService.GetUserIdForClaims();
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                Cart? cart = await _unitOfWork.Carts.GetCartByUserIdWithItemsAsync(userId);
                if (cart == null)
                {
                    throw new Exception("Cart not found");
                }

                CartItem? existItem = await _unitOfWork.Carts.GetCartItemByProductIdAsync(cart.Id, request.productId);
                if (existItem == null)
                {
                    throw new Exception("Product not found in cart");
                }

                // Remove Item
                await _unitOfWork.CartItem.Delete(existItem);
                cart.CartItems.Remove(existItem);

                // update total cart
                cart.TotalAmount = cart.CartItems.Sum(p => p.Quantity * p.Product.Price);
                await _unitOfWork.Carts.Update(cart);

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
