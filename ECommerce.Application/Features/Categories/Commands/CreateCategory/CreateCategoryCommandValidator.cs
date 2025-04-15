using FluentValidation;

namespace ECommerce.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Name is required");
            //RuleFor(t => t.Description)
            //    .NotEmpty().WithMessage("Description is required");
        }
    }
}
