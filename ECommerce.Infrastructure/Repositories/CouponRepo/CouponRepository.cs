using ECommerce.Application.Interfaces.Repositories.CouponRepository;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.CouponRepo
{
    public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
    {
        public CouponRepository(ApplicationDbContext db) : base(db) { }

        public async Task<Coupon?> GetCouponByIdWithItemsAsync(Guid couponId)
        {
            return await _db.Coupons
                .Include(t => t.Product_Coupons)
                .ThenInclude(t => t.Product)
                .FirstOrDefaultAsync(t => t.Id == couponId);
        }

        public async Task<List<Guid>> GetExistingProductIdsAsync(Guid couponId, List<Guid> productIds)
        {
            return await _db.Product_Coupons.Where(t => t.CouponId == couponId
                    && productIds.Contains(t.ProductId))
                .Select(cp => cp.ProductId).ToListAsync();
        }

        public async Task<Coupon?> GetValidCouponForProductAsync(Guid productId)
        {
            return await _db.Product_Coupons.Where(t => t.ProductId == productId)
                 .Where(c => c.Coupon.StartDate <= DateTime.UtcNow && DateTime.UtcNow <= c.Coupon.EndDate)
                 .Select(t => t.Coupon)
                 .FirstOrDefaultAsync();
        }
    }
}
