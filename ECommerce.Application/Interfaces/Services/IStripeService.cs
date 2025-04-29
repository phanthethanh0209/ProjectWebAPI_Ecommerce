using ECommerce.Application.Features.Payments.DTOs;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IStripeService
    {
        Task<StripePaymentIntentResult> CreatePaymentIntentAsync(Guid orderId, string currency, decimal totalAmount,
            string paymentMethodType);
        Task HandleWebhookEventAsync(string json, string stripeSignature, string webhookSecret);
    }
}
