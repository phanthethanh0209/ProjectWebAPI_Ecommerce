using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ResultResponse<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; } // FK

        //public CreateProductCommand(string name, string description, int stockQuantity, decimal price, int categoryId)
        //{
        //    Name = name;
        //    Description = description;
        //    StockQuantity = stockQuantity;
        //    Price = price;
        //    CategoryId = categoryId;
        //}
    }
}
