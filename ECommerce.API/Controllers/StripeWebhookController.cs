using ECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/stripe/webhook")]
    [ApiController]
    public class StripeWebhookController : ControllerBase
    {
        private readonly IStripeService _stripeService;
        private readonly string _webhookSecret;

        public StripeWebhookController(IStripeService stripeService, IConfiguration configuration)
        {
            _stripeService = stripeService;
            _webhookSecret = configuration["StripeSettings:WebhookSecret"];
        }

        [HttpPost]
        public async Task<IActionResult> HandleWebhook()
        {
            string json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                await _stripeService.HandleWebhookEventAsync(json, Request.Headers["Stripe-Signature"], _webhookSecret);
                return Ok();
            }
            catch (Stripe.StripeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
