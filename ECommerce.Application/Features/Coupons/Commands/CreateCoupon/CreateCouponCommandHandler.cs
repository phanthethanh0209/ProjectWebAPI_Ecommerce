using AutoMapper;
using ECommerce.Application.Common.Responses;
using ECommerce.Application.Interfaces.BackgroundJobs;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Coupons.Commands.CreateCoupon
{
    public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, ResultResponse<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBackgroundService _backgroundService;


        public CreateCouponCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IBackgroundService backgroundService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _backgroundService = backgroundService;
        }

        public async Task<ResultResponse<Guid>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            Coupon coupon = _mapper.Map<Coupon>(request);
            coupon.IsActive = false;

            await _unitOfWork.Coupons.AddAsync(coupon);
            await _unitOfWork.SaveChangesAsync();

            // send email notification to all user
            IEnumerable<User> users = await _unitOfWork.User.GetAllAsync();
            int batchSize = 10;

            for (int i = 0; i < users.Count(); i += batchSize)
            {
                List<User> batchUsers = users.Skip(i).Take(batchSize).ToList();

                // schedule send email notification 
                TimeSpan delay = coupon.StartDate - DateTime.UtcNow;
                _backgroundService.Schedule<ICouponBackgroundService>(t =>
                    t.SendNotificationCouponnEmail(batchUsers, coupon), delay);
            }

            // schedule update expired status coupon
            TimeSpan expiredDate = coupon.EndDate - DateTime.UtcNow;
            _backgroundService.Schedule<ICouponBackgroundService>(t =>
                t.ScheduleCouponExpiration(coupon.Id), expiredDate);

            return ResultResponse<Guid>.SuccessResponse(coupon.Id);
        }
    }
}
