namespace ECommerce.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
        Task<string> RenderTemplateAsync<T>(string templateName, T model);
    }
}
