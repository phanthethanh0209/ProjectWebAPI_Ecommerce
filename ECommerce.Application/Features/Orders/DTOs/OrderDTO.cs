namespace ECommerce.Application.Features.Orders.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ShippingAddress { get; set; }
        public string Status { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public List<OrderItems> OrderItems { get; set; }
    }

    public class OrderItems
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
