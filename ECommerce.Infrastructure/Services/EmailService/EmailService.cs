using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using RazorLight;

namespace ECommerce.Infrastructure.Services.EmailService
{
    internal class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RazorLightEngine _razorEngine;
        private readonly ILogger _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, IUnitOfWork unitOfWork, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _unitOfWork = unitOfWork;
            _logger = logger;

            _razorEngine = new RazorLightEngineBuilder()
            .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "Email"))
            .UseMemoryCachingProvider()
            .Build();
        }

        public async Task SendEmailAsync(string toEmail, string subject, string bodyHtml)
        {
            try
            {
                MimeMessage message = new();
                message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
                message.To.Add(MailboxAddress.Parse(toEmail));
                message.Subject = subject;
                message.Body = new TextPart("html") { Text = bodyHtml };

                using (SmtpClient smtpClient = new())
                {
                    await smtpClient.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                    await smtpClient.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                    await smtpClient.SendAsync(message);
                    await smtpClient.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send email: {ex.Message}", ex);
            }
        }

        public async Task<string> RenderTemplateAsync<T>(string templateName, T model)
        {
            return await _razorEngine.CompileRenderAsync(templateName, model);
        }
    }
}
