using FluentValidation;

namespace ECommerce.Application.Features.Authentication.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(e => e.email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress();
            RuleFor(e => e.password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
