namespace ECommerce.Infrastructure.BackgroundJobs.OrderBackgroundService
{
    public class OrderEmailDTO
    {
        public string Username { get; set; }
        public string OrderId { get; set; }
        public string TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string OrderDate { get; set; }
        public string ToEmail { get; set; }

        //public List<OrderItemDTO> Items { get; set; }
    }

    public class OrderItemDTO
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string UnitPrice { get; set; }
    }
}
