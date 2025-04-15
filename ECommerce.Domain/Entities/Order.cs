namespace ECommerce.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Payment Payment { get; set; } // 1 - 1 Payment
        public ICollection<OrderItem> OrderItems { get; set; }


    }
}
