using ECommerce.Application.Features.Payments.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<StripePaymentIntentResult>
    {
        public Guid OrderId { get; set; }
        //public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = "usd";
        public string PaymentMethod { get; set; } = "card";
    }
}
