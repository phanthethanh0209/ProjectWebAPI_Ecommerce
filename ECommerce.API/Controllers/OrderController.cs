using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Orders.Commands.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            ResultResponse<Guid> result = await _mediator.Send(command);
            //return result.IsSuccess ?
            //    CreatedAtAction(nameof(GetOrderById), new { id = result.Data }, result.Data) : BadRequest(result.ErrorMessage);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet]
        public Task<IActionResult> GetOrderById()
        {
            throw new NotImplementedException();
        }
    }
}
