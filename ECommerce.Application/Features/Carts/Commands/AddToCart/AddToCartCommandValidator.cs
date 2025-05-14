using FluentValidation;

namespace ECommerce.Application.Features.Carts.Commands.AddToCart
{
    public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
    {
        public AddToCartCommandValidator()
        {
            RuleFor(t => t.productId)
                .NotEmpty().WithMessage("Product id is required");

            RuleFor(t => t.quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero");
        }
    }
}
