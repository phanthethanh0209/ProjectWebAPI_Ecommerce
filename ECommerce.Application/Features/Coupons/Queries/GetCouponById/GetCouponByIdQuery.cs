using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Coupons.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Coupons.Queries.GetCouponById
{
    public record GetCouponByIdQuery(Guid couponId) : IRequest<ResultResponse<CouponResponse>>;
}
