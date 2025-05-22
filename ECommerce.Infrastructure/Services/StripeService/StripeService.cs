using ECommerce.Application.Features.Payments.DTOs;
using ECommerce.Application.Interfaces.BackgroundJobs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
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

            Order order = await _unitOfWork.Orders.GetFirstOrDefaultAsync(t => t.Id.Equals(payment.OrderId), items => items.OrderItems);

            if (stripeEvent.Type == "payment_intent.succeeded")
            {
                // update payment and order status 
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    payment.PaymentStatus = PaymentStatus.Completed;
                    await _unitOfWork.Payment.Update(payment);

                    order.Status = OrderStatus.Paid;
                    order.UpdateAt = DateTime.UtcNow;
                    await _unitOfWork.Orders.Update(order);

                    // update quantity product
                    foreach (OrderItem item in order.OrderItems)
                    {
                        Domain.Entities.Product product = await _unitOfWork.Product.GetFirstOrDefaultAsync(t => t.Id.Equals(item.ProductId));
                        if (product == null)
                            throw new Exception($"Product with ID {item.ProductId} not found");

                        product.StockQuantity -= item.Quantity;
                        await _unitOfWork.Product.Update(product);
                    }

                    // remove item in cart and update total cart
                    Cart cart = await _unitOfWork.Carts.GetFirstOrDefaultAsync(t => t.UserId == order.UserId);
                    await _unitOfWork.Carts.RemoveCartItemsAsync(cart.Id, order.OrderItems.Select(t => t.ProductId).ToList());
                    cart.TotalAmount -= order.TotalAmount;
                    await _unitOfWork.Carts.Update(cart);

                    // send email with mailkit use backgroundjob (fire and forget)
                    _backgroundJobClient.Enqueue<IOrderBackgroundService>(x => x.SendOrderConfirmationEmail(order.Id));

                    await _unitOfWork.CommitTransactionAsync();
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw new Exception($"Failed to handle payment failed event: {ex.Message}", ex);
                }
            }
            else if (stripeEvent.Type == "payment_intent.payment_failed")
            {
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    payment.PaymentStatus = PaymentStatus.Failed;
                    await _unitOfWork.Payment.Update(payment);

                    order.Status = OrderStatus.Failed;
                    order.UpdateAt = DateTime.UtcNow;
                    await _unitOfWork.Orders.Update(order);

                    await _unitOfWork.CommitTransactionAsync();
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw new Exception($"Failed to handle payment failed event: {ex.Message}", ex);
                }
            }
        }
    }
}
