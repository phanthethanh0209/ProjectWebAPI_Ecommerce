namespace ECommerce.Application.Features.Carts.DTOs
{
    public class CartItemDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
