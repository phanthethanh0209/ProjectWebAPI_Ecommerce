using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Orders.DTOs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStripeService _stripeService;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IStripeService stripeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stripeService = stripeService;
        }

        public async Task<ResultResponse<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                decimal total = 0;
                // add order
                Order order = _mapper.Map<Order>(request);
                order.Status = "Pending";
                await _unitOfWork.Order.AddAsync(order);


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
                    OrderItem orderItem = _mapper.Map<OrderItem>(item);
                    orderItem.Price = product.Price;
                    orderItem.OrderId = order.Id;
                    await _unitOfWork.OrderItem.AddAsync(orderItem);

                    // update quantity product
                    product.StockQuantity -= item.Quantity;
                    await _unitOfWork.Product.Update(product);

                    // set total amount for order
                    total += product.Price * item.Quantity;
                }

                order.TotalAmount = total;

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
