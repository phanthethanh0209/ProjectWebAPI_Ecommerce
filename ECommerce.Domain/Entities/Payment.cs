namespace ECommerce.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public string StripePaymentIntentId { get; set; } // ID từ Stripe
        public string PaymentMethod { get; set; } // Credit Card, PayPal, etc.
        public string PaymentStatus { get; set; }// Pending, Paid, Failed
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
