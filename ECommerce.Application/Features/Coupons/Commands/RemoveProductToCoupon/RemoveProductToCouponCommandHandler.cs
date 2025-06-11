using AutoMapper;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Coupons.Commands.RemoveProductToCoupon
{
    public class RemoveProductToCouponCommandHandler
        : IRequestHandler<RemoveProductToCouponCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RemoveProductToCouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<Guid>> Handle(RemoveProductToCouponCommand request, CancellationToken cancellationToken)
        {
            Coupon coupon = await _unitOfWork.Coupons.GetFirstOrDefaultAsync(t => t.Id == request.couponId);
            if (coupon == null)
            {
                throw new NotFoundException("Coupon", request.couponId);
            }

            Product_Coupons products = await _unitOfWork.Product_Coupon
                                                        .GetFirstOrDefaultAsync(t => t.CouponId == request.couponId
                                                         && t.ProductId == request.productId);
            if (products == null)
            {
                throw new NotFoundException("Product", request.productId);
            }

            await _unitOfWork.Product_Coupon.Delete(products);
            await _unitOfWork.SaveChangesAsync();

            return ResultResponse<Guid>.SuccessResponse(coupon.Id);

        }
    }
}
