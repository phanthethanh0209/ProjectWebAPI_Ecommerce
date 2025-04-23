namespace ECommerce.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public Guid UserId { get; set; } // FK
        public User User { get; set; } // Navigation Property
    }
}
