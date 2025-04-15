using FluentValidation;

namespace ECommerce.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Name is required");
            //RuleFor(t => t.Description)
            //    .NotEmpty().WithMessage("Description is required");
        }
    }
}
