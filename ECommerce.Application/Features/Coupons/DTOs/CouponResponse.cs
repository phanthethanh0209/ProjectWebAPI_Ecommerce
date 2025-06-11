namespace ECommerce.Application.Features.Coupons.DTOs
{
    public class CouponResponse
    {
        public string Name { get; set; }
        public string CouponType { get; set; }
        public decimal CouponValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public List<ProductDTO>? Products { get; set; }
    }

    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
