namespace ECommerce.Domain.Entities
{
    public class Product_Coupons
    {
        public Guid CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
