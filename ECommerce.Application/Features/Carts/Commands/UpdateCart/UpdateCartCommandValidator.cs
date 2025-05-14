using FluentValidation;

namespace ECommerce.Application.Features.Carts.Commands.UpdateCart
{
    public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(t => t.productId)
                .NotEmpty().WithMessage("Product id is required");

            RuleFor(t => t.quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero");
        }
    }
}
