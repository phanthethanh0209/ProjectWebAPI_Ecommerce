namespace ECommerce.Application.Features.Products.DTOs
{
    public class GetProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid CategoryId { get; set; } // FK
    }
}
