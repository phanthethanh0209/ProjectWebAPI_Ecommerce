using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Interfaces.Repositories;
using FluentValidation;

namespace ECommerce.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


            //RuleFor(x => x.Id)
            //    .MustAsync(ProductExists).WithMessage("Product not found");
            RuleFor(x => x.Id).NotEmpty().WithMessage("ProductId is required");

        }

        //private async Task<bool> ProductExists(Guid id, CancellationToken cancellationToken)
        //{
        //    Product? product = await _unitOfWork.Product.GetFirstOrDefaultAsync(e => e.Id == id);
        //    return product is not null;
        //}
    }
}
