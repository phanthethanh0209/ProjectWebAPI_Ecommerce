using ECommerce.Application.Common.Responses;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ResultResponse<Guid>>
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; } // FK


        //public UpdateProductCommand(int id)
        //{
        //    Id = id;
        //    Name = null;
        //    Description = null;
        //    StockQuantity = 0;
        //    Price = 0;
        //    CategoryId = 0;
        //}

    }
}
