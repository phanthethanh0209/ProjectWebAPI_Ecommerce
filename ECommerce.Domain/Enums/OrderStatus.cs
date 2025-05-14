namespace ECommerce.Domain.Enums
{
    public enum OrderStatus
    {
        Pending,
        Paid,
        Cancelled,
        Failed, // payment failed
        Completed,

    }
}
