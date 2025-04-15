using ECommerce.Application.Interfaces.Repositories;
using FluentValidation;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(50).WithMessage("The length of the name can't be more than 50 characters long");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required")
                .MaximumLength(100).WithMessage("The length of the description can't be more than 100 characters long");

            RuleFor(x => x.StockQuantity).GreaterThan(0).WithMessage("Price must be greater than zero");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");

            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required");

            //RuleFor(x => x.CategoryId).MustAsync(CategoryExists).WithMessage("Category Id not exists");
        }

        //private async Task<bool> CategoryExists(Guid categoryId, CancellationToken cancellationToken)
        //{
        //    return await _unitOfWork.Category.AnyAsync(e => e.Id == categoryId);
        //}
    }
}
