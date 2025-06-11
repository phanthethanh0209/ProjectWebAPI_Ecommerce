using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Coupons.DTOs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Coupons.Queries.GetCouponById
{
    public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, ResultResponse<CouponResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCouponByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<CouponResponse>> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            Coupon? coupon = await _unitOfWork.Coupons.GetCouponByIdWithItemsAsync(request.couponId);
            if (coupon == null)
            {
                throw new NotFoundException("Coupon", request.couponId);
            }

            CouponResponse data = _mapper.Map<CouponResponse>(coupon);
            return ResultResponse<CouponResponse>.SuccessResponse(data);
        }
    }
}
