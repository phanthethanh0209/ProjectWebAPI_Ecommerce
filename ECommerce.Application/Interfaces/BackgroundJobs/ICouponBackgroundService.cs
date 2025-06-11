using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.BackgroundJobs
{
    public interface ICouponBackgroundService
    {
        Task SendNotificationCouponnEmail(List<User> batchUsers, Coupon coupon);
        Task ScheduleCouponExpiration(Guid coupon);

    }
}
