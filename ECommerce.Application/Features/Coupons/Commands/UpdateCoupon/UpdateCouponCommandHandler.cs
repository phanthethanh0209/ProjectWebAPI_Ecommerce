using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Coupons.Commands.UpdateCoupon
{
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, ResultResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse<Guid>> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            Coupon coupon = await _unitOfWork.Coupons.GetFirstOrDefaultAsync(t => t.Id == request.Id && t.IsActive);
            if (coupon == null)
            {
                throw new Exception("The coupon has expired and cannot be update");
            }

            _mapper.Map(request, coupon);
            await _unitOfWork.Coupons.Update(coupon);
            await _unitOfWork.SaveChangesAsync();

            return ResultResponse<Guid>.SuccessResponse(coupon.Id);
        }
    }
}
