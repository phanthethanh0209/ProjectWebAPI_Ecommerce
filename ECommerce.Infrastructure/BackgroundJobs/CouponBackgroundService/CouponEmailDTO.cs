namespace ECommerce.Infrastructure.BackgroundJobs.CouponBackgroundService
{
    public class CouponEmailDTO
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string DiscountValue { get; set; }
        public string DiscountType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ShopLink { get; set; }
        public string ToEmail { get; set; }
    }
}
