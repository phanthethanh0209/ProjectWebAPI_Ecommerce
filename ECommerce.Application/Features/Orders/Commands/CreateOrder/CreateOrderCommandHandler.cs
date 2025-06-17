using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Orders.DTOs;
using ECommerce.Application.Interfaces.Authentication;
using ECommerce.Application.Interfaces.BackgroundJobs;
using ECommerce.Application.Interfaces.Pricing;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStripeService _stripeService;
        private readonly IBackgroundService _backgroundService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICouponCalculator _couponCalculator;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IStripeService stripeService,
            IBackgroundService backgroundService, ICurrentUserService currentUserService,
            ICouponCalculator couponCalculator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stripeService = stripeService;
            _backgroundService = backgroundService;
            _currentUserService = currentUserService;
            _couponCalculator = couponCalculator;
        }

        public async Task<ResultResponse<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Guid userId = _currentUserService.GetUserIdForClaims();

            // check product from request with product from cart
            Cart? cart = await _unitOfWork.Carts.GetCartByUserIdWithItemsAsync(userId);
            if (cart == null || !cart.CartItems.Any())
            {
                throw new Exception("Cart not found");
            }

            bool isMatch = request.OrderItems.All(r =>
            {
                CartItem? cartItem = cart.CartItems.FirstOrDefault(t => t.ProductId == r.ProductId);
                return cartItem != null && cartItem.Quantity == r.Quantity;
            });

            if (!isMatch)
            {
                throw new Exception("Cart items do not match the request");
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // add order
                decimal total = 0;
                Order order = _mapper.Map<Order>(request);
                order.Status = OrderStatus.Pending;
                order.UserId = userId;
                await _unitOfWork.Orders.AddAsync(order);

                // add order items
                foreach (OrderItemDTO item in request.OrderItems)
                {
                    // check product out of stock
                    Product product = await _unitOfWork.Product.GetFirstOrDefaultAsync(t => t.Id.Equals(item.ProductId));
                    if (product == null)
                        throw new Exception($"Product with ID {item.ProductId} not found");

                    if (product.StockQuantity < item.Quantity)
                        throw new Exception($"Not enough stock for product {product.Id}");

                    // add order item
                    var discountAmount = await _couponCalculator.CalculateDiscountAmount(product);
                    OrderItem orderItem = _mapper.Map<OrderItem>(item);
                    orderItem.Price = product.Price - discountAmount;
                    orderItem.OrderId = order.Id;
                    await _unitOfWork.OrderItem.AddAsync(orderItem);

                    // set total amount for order
                    total += orderItem.Price * item.Quantity;
                }

                order.TotalAmount = total;

                // setup delayed job (scheduled job with hangfire)
                _backgroundService.Schedule<IOrderBackgroundService>(t => t.ScheduleCancelOrderJob(order.Id), TimeSpan.FromMinutes(15));

                // save change and commit
                await _unitOfWork.CommitTransactionAsync();
                return ResultResponse<Guid>.SuccessResponse(order.Id);
            }
            catch
            {
                // rollback if there is error
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
