namespace ECommerce.Application.Features.Orders.DTOs
{
    public class CreateOrderItemDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
