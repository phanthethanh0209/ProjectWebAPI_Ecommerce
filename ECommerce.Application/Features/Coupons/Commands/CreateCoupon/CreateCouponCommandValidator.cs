using ECommerce.Application.Features.Coupons.Commands.Common;

namespace ECommerce.Application.Features.Coupons.Commands.CreateCoupon
{
    public class CreateCouponCommandValidator : CouponCommandValidator<CreateCouponCommand>
    {
        public CreateCouponCommandValidator()
        {
            // có thể viết rule riêng nếu có
        }
    }
}
