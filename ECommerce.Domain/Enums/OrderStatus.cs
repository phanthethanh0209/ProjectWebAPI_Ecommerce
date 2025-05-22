namespace ECommerce.Domain.Enums
{
    public enum OrderStatus
    {
        Pending = 0,
        Paid = 1,
        //Shipped = 2,
        Completed = 2,
        Cancelled = 3,
        Failed = 4, // payment failed
    }
}
