using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Coupons.Commands.AddProductToCoupon
{
    public class AddProductToCouponCommandHandler : IRequestHandler<AddProductToCouponCommand, ResultResponse<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductToCouponCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultResponse<Guid>> Handle(AddProductToCouponCommand request, CancellationToken cancellationToken)
        {
            Coupon coupon = await _unitOfWork.Coupons.GetFirstOrDefaultAsync(t => t.Id == request.CouponId);
            if (coupon == null)
            {
                throw new Exception("The coupon not found");
            }

            if (DateTime.UtcNow > coupon.EndDate)
            {
                throw new Exception("The coupon has expired and cannot be used");
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // check existing products
                List<Guid> existingProduct = await _unitOfWork.Product.GetExistingProductAsync(request.ProductIds);
                IEnumerable<Guid> notFoundIds = request.ProductIds.Except(existingProduct);
                if (notFoundIds.Any())
                    throw new Exception($"Some product not found: {string.Join(", ", notFoundIds)}");

                // check existing products in current coupon --> ignore
                List<Guid> existingCouponProduct = await _unitOfWork.Coupons.GetExistingProductIdsAsync(coupon.Id, request.ProductIds.ToList());
                IEnumerable<Guid> CouponProductNotExist = request.ProductIds.Except(existingCouponProduct);

                foreach (Guid item in CouponProductNotExist)
                {
                    // check existing products in other coupon on startDate -> endDate
                    Coupon? couponOtherActive = await _unitOfWork.Coupons.GetValidCouponForProductAsync(item);
                    if (couponOtherActive != null)
                    {
                        throw new Exception($"Product {item} existing in other active coupon '{couponOtherActive.Id}'");
                    }

                    await _unitOfWork.Product_Coupon.AddAsync(new Product_Coupons
                    {
                        CouponId = request.CouponId,
                        ProductId = item,
                    });
                }

                await _unitOfWork.CommitTransactionAsync();
                return ResultResponse<Guid>.SuccessResponse(coupon.Id);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
