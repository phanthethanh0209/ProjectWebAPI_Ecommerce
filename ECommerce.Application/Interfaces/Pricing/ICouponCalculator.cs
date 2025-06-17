using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Interfaces.Pricing
{
    public interface ICouponCalculator
    {
        public decimal CalculateDiscount(decimal originalPrice, CouponType type, decimal value);
        public Task<decimal> CalculateDiscountAmount(Product product);
    }
}
