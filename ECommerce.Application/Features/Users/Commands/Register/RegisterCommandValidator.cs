using ECommerce.Application.Interfaces.Repositories;
using FluentValidation;

namespace ECommerce.Application.Features.Users.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(e => e.name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name can't exceed 100 characters.");

            RuleFor(e => e.email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(BeUniqueEmail).WithMessage("Email already exists.");

            RuleFor(e => e.password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("Password must contain at least one special character.");

            RuleFor(e => e.phone)
                .NotEmpty().WithMessage("Phone is required")
                .Matches(@"^0\d{9}$").WithMessage("Phone must start with 0 and have 10 digits.");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.User.AnyAsync(u => u.Email == email);
        }
    }
}
