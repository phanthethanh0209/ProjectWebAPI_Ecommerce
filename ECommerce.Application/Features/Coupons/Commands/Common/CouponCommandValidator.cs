using FluentValidation;

namespace ECommerce.Application.Features.Coupons.Commands.Common
{
    public abstract class CouponCommandValidator<T> : AbstractValidator<T> where T : ICouponRequest
    {
        public CouponCommandValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Coupon name is required")
                .MaximumLength(50).WithMessage("The length of the name can't be more than 50 characters long");

            RuleFor(x => x.CouponValue)
                .GreaterThan(0).WithMessage("Coupon value must be greater than zero");

            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required");

            RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date is required");

            RuleFor(x => x.StartDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Start date must be greater than current date");

            RuleFor(x => x.EndDate)
                .GreaterThan(e => e.StartDate)
                .WithMessage("End date must be greater than start date");

        }
    }
}
