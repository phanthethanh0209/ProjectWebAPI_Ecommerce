using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Authentication;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Carts.Commands.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public AddToCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<ResultResponse<Guid>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            Guid userId = _currentUserService.GetUserIdForClaims();

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                Product? product = await _unitOfWork.Product.GetFirstOrDefaultAsync(t => t.Id == request.productId);
                if (product == null)
                    throw new Exception($"Product with ID {request.productId} not found");

                Cart? cart = await _unitOfWork.Carts.GetCartByUserIdWithItemsAsync(userId);
                if (cart == null)
                {
                    throw new Exception("Cart not found");

                    // new cart
                    //cart = new()
                    //{
                    //    UserId = userId,
                    //    TotalAmount = 0,
                    //    CartItems = new List<CartItem>()
                    //};
                    //await _unitOfWork.Carts.AddAsync(cart);
                }

                // check exist product, -> increase quantity
                CartItem? existItem = await _unitOfWork.Carts.GetCartItemByProductIdAsync(cart.Id, request.productId);
                int totalQuantity = 0;
                if (existItem != null)
                {
                    existItem.Quantity += request.quantity;

                    totalQuantity = existItem.Quantity;
                    await _unitOfWork.Carts.Update(cart);
                }
                else
                {
                    CartItem cartItem = _mapper.Map<CartItem>(request);
                    cartItem.CartId = cart.Id;

                    totalQuantity = cartItem.Quantity;
                    await _unitOfWork.CartItem.AddAsync(cartItem);
                }

                // check quantity product
                if (product.StockQuantity < totalQuantity)
                {
                    throw new Exception($"Not enough stock for product {product.Id}");
                }

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
