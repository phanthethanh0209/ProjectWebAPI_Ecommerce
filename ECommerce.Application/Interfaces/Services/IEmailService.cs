namespace ECommerce.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
        Task SendEmailBatchAsync(IEnumerable<string> emails, string subject, string body);
        Task<string> RenderTemplateAsync<T>(string templateName, T model);
    }
}
