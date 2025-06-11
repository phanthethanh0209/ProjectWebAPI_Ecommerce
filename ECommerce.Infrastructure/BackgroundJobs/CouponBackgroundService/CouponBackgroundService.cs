using ECommerce.Application.Interfaces.BackgroundJobs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure.BackgroundJobs.CouponBackgroundService
{
    public class CouponBackgroundService : ICouponBackgroundService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<CouponBackgroundService> _logger;

        public CouponBackgroundService(IUnitOfWork unitOfWork, IEmailService emailService, ILogger<CouponBackgroundService> logger)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task SendNotificationCouponnEmail(List<User> batchUsers, Coupon coupon)
        {
            //IEnumerable<Task> emailTask = batchUsers.Select(async user =>
            //{
            //    try
            //    {
            //        CouponEmailDTO model = new()
            //        {
            //            UserName = user.Name,
            //            Name = coupon.Name,
            //            DiscountValue = coupon.CouponValue.ToString(),
            //            DiscountType = coupon.CouponType.ToString(),
            //            StartDate = coupon.StartDate.ToString("dd/MM/yyyy HH:mm:ss"),
            //            EndDate = coupon.EndDate.ToString("dd/MM/yyyy HH:mm:ss"),
            //            ToEmail = user.Email,
            //            ShopLink = ""
            //        };

            //        string templateName = "PromotionNotificationTemplate.cshtml";

            //        string body = await _emailService.RenderTemplateAsync(templateName, model);
            //        string subject = "Special Promotion for You 💌🎉";

            //        await _emailService.SendEmailAsync(model.ToEmail, subject, body);
            //        _logger.LogInformation($"Successfully sent email to {user.Email}");
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError($"Failed to send email to {user.Email}: {ex.Message}");
            //    }
            //});

            //await Task.WhenAll(emailTask);

            if (!coupon.IsActive)
            {
                coupon.IsActive = true;
                coupon.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Coupons.Update(coupon);
                await _unitOfWork.SaveChangesAsync();
            }

            foreach (User user in batchUsers)
            {
                try
                {
                    CouponEmailDTO model = new()
                    {
                        UserName = user.Name,
                        Name = coupon.Name,
                        DiscountValue = coupon.CouponValue.ToString(),
                        DiscountType = coupon.CouponType.ToString(),
                        StartDate = coupon.StartDate.ToString("dd/MM/yyyy HH:mm:ss"),
                        EndDate = coupon.EndDate.ToString("dd/MM/yyyy HH:mm:ss"),
                        ToEmail = user.Email,
                        ShopLink = ""
                    };

                    string templateName = "PromotionNotificationTemplate.cshtml";

                    string body = await _emailService.RenderTemplateAsync(templateName, model);
                    string subject = "Special Promotion for You 💌🎉";

                    await _emailService.SendEmailAsync(model.ToEmail, subject, body);
                    _logger.LogInformation($"Successfully sent email to {user.Email}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to send email to {user.Email}: {ex.Message}");
                }
            }
        }

        public async Task ScheduleCouponExpiration(Guid couponId)
        {
            Coupon coupon = await _unitOfWork.Coupons.GetFirstOrDefaultAsync(t => t.Id == couponId);
            if (coupon == null || !coupon.IsActive)
            {
                // log
                return;
            }

            coupon.IsActive = false;
            coupon.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Coupons.Update(coupon);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
