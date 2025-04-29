namespace ECommerce.Application.Features.Payments.DTOs
{
    public record class StripePaymentIntentResult(string ClientSecret, string PaymentIntentId);

}
