using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Coupons.Commands.RemoveProductToCoupon
{
    public record RemoveProductToCouponCommand(Guid couponId, Guid productId) : IRequest<ResultResponse<Guid>>;
}
