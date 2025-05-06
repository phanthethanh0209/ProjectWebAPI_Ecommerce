using ECommerce.Application.Features.Payments.DTOs;
using ECommerce.Application.Interfaces.BackgroundJobs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace ECommerce.Infrastructure.Services.StripeService
{
    public class StripeService : IStripeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public StripeService(IConfiguration config, IUnitOfWork unitOfWork, IBackgroundJobClient backgroundJobClient)
        {
            StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];
            _unitOfWork = unitOfWork;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<StripePaymentIntentResult> CreatePaymentIntentAsync(Guid orderId, string currency, decimal totalAmount,
            string paymentMethodType)
        {
            long amount = 0;

            if (currency.ToLower() == "vnd")
                amount = (long)totalAmount; // For VND not * 100
            else
                amount = (long)(totalAmount * 100);// if currency is "usd" or "eur"

            PaymentIntentCreateOptions options = new()
            {
                Amount = amount,
                Currency = currency,
                PaymentMethodTypes = new List<string> { paymentMethodType },
                Description = $"Order for {orderId}"
            };

            PaymentIntentService service = new();
            PaymentIntent intent = await service.CreateAsync(options);
            return new StripePaymentIntentResult(intent.ClientSecret, intent.Id);
        }

        public async Task HandleWebhookEventAsync(string json, string stripeSignature, string webhookSecret)
        {
            Event stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, webhookSecret); // xác thực tính hợp lệ của request bằng webhookSecret

            PaymentIntent? paymentIntent = stripeEvent.Data.Object as PaymentIntent; // Lấy PaymentIntent từ dữ liệu sự kiện Stripe
            if (paymentIntent == null) return;

            Payment payment = await _unitOfWork.Payment.GetFirstOrDefaultAsync(t => t.StripePaymentIntentId.Equals(paymentIntent.Id));

            if (payment == null) return;
            Order order = await _unitOfWork.Order.GetFirstOrDefaultAsync(t => t.Id.Equals(payment.OrderId));

            if (stripeEvent.Type == "payment_intent.succeeded")
            {
                // update payment and order status 
                payment.PaymentStatus = "Completed";
                await _unitOfWork.Payment.Update(payment);

                order.Status = "Processing";
                order.UpdateAt = DateTime.UtcNow;
                await _unitOfWork.Order.Update(order);

                await _unitOfWork.SaveChangesAsync();

                _backgroundJobClient.Enqueue<IOrderBackgroundService>(x => x.SendOrderConfirmationEmail(order.Id));
            }
            else if (stripeEvent.Type == "payment_intent.payment_failed")
            {
                payment.PaymentStatus = "Failed";
                await _unitOfWork.Payment.Update(payment);

                order.Status = "PaymentFailed";
                order.UpdateAt = DateTime.UtcNow;
                await _unitOfWork.Order.Update(order);

                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
