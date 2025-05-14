using ECommerce.Application.Interfaces.BackgroundJobs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using Microsoft.Extensions.Logging;
using Stripe;
using System.Globalization;

namespace ECommerce.Infrastructure.BackgroundJobs.OrderBackgroundService
{
    public class OrderBackgroundService : IOrderBackgroundService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<OrderBackgroundService> _logger;


        public OrderBackgroundService(IUnitOfWork unitOfWork, ILogger<OrderBackgroundService> logger,
            IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task SendOrderConfirmationEmail(Guid orderId)
        {
            _logger.LogInformation("Current Dir: {Dir}", Path.Combine(Directory.GetCurrentDirectory(), "Templates", "Email"));

            Order order = await _unitOfWork.Order.GetFirstOrDefaultAsync(t => t.Id == orderId, items => items.OrderItems);
            if (order == null)
                throw new Exception("Order not found");

            User user = await _unitOfWork.User.GetFirstOrDefaultAsync(t => t.Id == order.UserId);
            if (user == null)
                throw new Exception("User not found");

            OrderEmailDTO model = new()
            {
                Username = user.Name,
                OrderId = order.Id.ToString(),
                TotalAmount = order.TotalAmount.ToString("C0", new CultureInfo("vi-VN")),
                ShippingAddress = order.ShippingAddress,
                OrderDate = order.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
                ToEmail = user.Email,
                //Items = order.OrderItems.Select(item => new OrderItemDTO
                //{
                //    ProductName = item.Product.Name,
                //    Quantity = item.Quantity,
                //    UnitPrice = item.Price.ToString("C0", new CultureInfo("vi-VN")),
                //}).ToList()
            };

            string templateName = "OrderConfirmationTemplate.cshtml";

            string body = await _emailService.RenderTemplateAsync(templateName, model);
            string subject = "Payment Successful - Your Order is Being Processed";

            await _emailService.SendEmailAsync(model.ToEmail, subject, body);

        }

        public async Task ScheduleCancelOrderJob(Guid orderId)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                Order order = await _unitOfWork.Order.GetFirstOrDefaultAsync(t => t.Id == orderId);
                if (order == null || order.Status != "Pending")
                {
                    // log
                    return;
                }

                Payment payment = await _unitOfWork.Payment.GetFirstOrDefaultAsync(t => t.OrderId == orderId);
                if (payment == null || payment.PaymentStatus != "Pending")
                {
                    // log
                    return;
                }

                // update status Order and Payment
                order.Status = "Cancelled";
                order.UpdateAt = DateTime.UtcNow;

                payment.PaymentStatus = "Cancelled";

                await _unitOfWork.Order.Update(order);
                await _unitOfWork.Payment.Update(payment);

                // update Stripe
                PaymentIntentService service = new();
                await service.CancelAsync(payment.StripePaymentIntentId);

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                // log
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

        }

    }
}
