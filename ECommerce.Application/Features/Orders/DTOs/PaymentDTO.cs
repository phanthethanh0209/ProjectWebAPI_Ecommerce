namespace ECommerce.Application.Features.Orders.DTOs
{
    public class PaymentDTO
    {
        public string PaymentMethod { get; set; } // Credit Card, PayPal, etc.
        public string PaymentStatus { get; set; }// Pending, Paid, Failed
    }
}
