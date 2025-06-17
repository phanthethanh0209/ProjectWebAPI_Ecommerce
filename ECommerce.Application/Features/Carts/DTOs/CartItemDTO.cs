namespace ECommerce.Application.Features.Carts.DTOs
{
    public class CartItemDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalUnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
