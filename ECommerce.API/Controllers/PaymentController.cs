using ECommerce.Application.Features.Payments.Commands.CreatePayment;
using ECommerce.Application.Features.Payments.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand command)
        {
            StripePaymentIntentResult result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
