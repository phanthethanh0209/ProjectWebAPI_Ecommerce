using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities
{
    public class Coupon
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CouponType CouponType { get; set; }
        public decimal CouponValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Product_Coupons> Product_Coupons { get; set; }
    }
}
