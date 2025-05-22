using ECommerce.Application.Interfaces.Repositories;
using FluentValidation;

namespace ECommerce.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name can't exceed 100 characters.");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format.");
            //.MustAsync(BeUniqueEmail).WithMessage("Email already exists.");

            RuleFor(e => e.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .Matches(@"^0\d{9}$").WithMessage("Phone must start with 0 and have 10 digits.");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.User.AnyAsync(u => u.Email == email);
        }
    }
}
