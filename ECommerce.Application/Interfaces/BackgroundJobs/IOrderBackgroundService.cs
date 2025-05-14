namespace ECommerce.Application.Interfaces.BackgroundJobs
{
    public interface IOrderBackgroundService
    {
        Task SendOrderConfirmationEmail(Guid orderId);
        Task ScheduleCancelOrderJob(Guid orderId);

    }
}
