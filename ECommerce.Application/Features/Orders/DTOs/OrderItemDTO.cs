namespace ECommerce.Application.Features.Orders.DTOs
{
    public class OrderItemDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
