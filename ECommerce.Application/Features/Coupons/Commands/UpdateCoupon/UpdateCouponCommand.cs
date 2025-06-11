using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Coupons.Commands.Common;
using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Application.Features.Coupons.Commands.UpdateCoupon
{
    public class UpdateCouponCommand : ICouponRequest, IRequest<ResultResponse<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CouponType CouponType { get; set; }
        public decimal CouponValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
