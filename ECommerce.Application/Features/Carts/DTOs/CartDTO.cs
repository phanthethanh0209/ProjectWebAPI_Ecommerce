namespace ECommerce.Application.Features.Carts.DTOs
{
    public class CartDTO
    {
        public Guid Id { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<CartItemDTO> CartItems { get; set; }
    }
}
