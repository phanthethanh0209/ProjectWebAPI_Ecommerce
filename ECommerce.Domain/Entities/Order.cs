namespace ECommerce.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string ShippingAddress { get; set; }
        //public OrderStatus Status { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; }

        public Payment Payment { get; set; } // 1 - 1 Payment
        public ICollection<OrderItem> OrderItems { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
