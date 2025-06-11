using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Coupons.Commands.AddProductToCoupon
{
    public class AddProductToCouponCommand : IRequest<ResultResponse<Guid>>
    {
        public Guid CouponId { get; set; }
        public List<Guid> ProductIds { get; set; }
    }
}
