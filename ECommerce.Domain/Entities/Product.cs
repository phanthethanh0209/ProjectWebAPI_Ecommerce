namespace ECommerce.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid CategoryId { get; set; } // FK
        public Category Category { get; set; } // Navigation Property

        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
