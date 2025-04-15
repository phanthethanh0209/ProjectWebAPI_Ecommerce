namespace ECommerce.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public ICollection<CartItem> CartItems { get; set; }

    }
}
