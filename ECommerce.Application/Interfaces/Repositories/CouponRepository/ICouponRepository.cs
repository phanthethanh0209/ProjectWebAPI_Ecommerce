using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories.CouponRepository
{
    public interface ICouponRepository : IGenericRepository<Coupon>
    {
        Task<List<Guid>> GetExistingProductIdsAsync(Guid couponId, List<Guid> productIds);
        Task<Coupon?> GetCouponByIdWithItemsAsync(Guid couponId);

    }
}
