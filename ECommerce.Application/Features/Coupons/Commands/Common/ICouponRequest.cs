using ECommerce.Domain.Enums;

namespace ECommerce.Application.Features.Coupons.Commands.Common
{
    public interface ICouponRequest
    {
        public string Name { get; set; }
        public CouponType CouponType { get; set; }
        public decimal CouponValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
