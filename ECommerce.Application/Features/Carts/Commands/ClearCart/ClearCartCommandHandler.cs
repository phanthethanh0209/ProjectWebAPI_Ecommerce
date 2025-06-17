using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Authentication;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Carts.Commands.ClearCart
{
    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, ResultResponse<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public ClearCartCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<ResultResponse<Unit>> Handle(ClearCartCommand request, CancellationToken cancellationToken)
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

                await _unitOfWork.Carts.ClearCartAsync(cart.Id);
                // update total cart
                //cart.TotalAmount = 0;
                await _unitOfWork.Carts.Update(cart);
                await _unitOfWork.CommitTransactionAsync();

                return ResultResponse<Unit>.SuccessResponse(Unit.Value);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
