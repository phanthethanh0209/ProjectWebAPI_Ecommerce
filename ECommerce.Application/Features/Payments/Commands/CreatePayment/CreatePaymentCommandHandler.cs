using AutoMapper;
using ECommerce.Application.Features.Payments.DTOs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, StripePaymentIntentResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStripeService _stripeService;

        public CreatePaymentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IStripeService stripeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stripeService = stripeService;
        }

        public async Task<StripePaymentIntentResult> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            // check exists order
            Order order = await _unitOfWork.Order.GetFirstOrDefaultAsync(t => t.Id == request.OrderId);
            if (order == null)
                throw new Exception("Order not found");

            if (order.Status != "Pending")
                throw new Exception("Order is not in a valid state for payment");

            // create paymentIntent in Stripe
            StripePaymentIntentResult paymentIntent = await _stripeService.CreatePaymentIntentAsync(order.Id, request.Currency,
                order.TotalAmount, request.PaymentMethod);

            // add payment
            Payment payment = new()
            {
                OrderId = order.Id,
                PaymentStatus = "Pending",
                PaymentMethod = request.PaymentMethod,
                StripePaymentIntentId = paymentIntent.PaymentIntentId,
            };
            await _unitOfWork.Payment.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            return paymentIntent;
        }
    }
}
