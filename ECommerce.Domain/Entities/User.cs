namespace ECommerce.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public Cart Cart { get; set; } // 1 - 1 Cart
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
