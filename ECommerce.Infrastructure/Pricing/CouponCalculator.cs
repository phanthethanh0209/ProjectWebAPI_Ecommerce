using ECommerce.Application.Interfaces.Pricing;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;

namespace ECommerce.Infrastructure.Pricing
{
    public class CouponCalculator : ICouponCalculator
    {
        private readonly IUnitOfWork _unitOfWork;

        public CouponCalculator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public decimal CalculateDiscount(decimal originalPrice, CouponType type, decimal value)
        {
            return type switch
            {
                CouponType.Percent => originalPrice * value / 100,
                CouponType.Fixed_amount => value,
                _ => 0
            };
        }

        public async Task<decimal> CalculateDiscountAmount(Product product)
        {
            Coupon? coupon = await _unitOfWork.Coupons.GetValidCouponForProductAsync(product.Id);
            return coupon == null ? 0 : CalculateDiscount(product.Price, coupon.CouponType, coupon.CouponValue);
        }
    }
}
