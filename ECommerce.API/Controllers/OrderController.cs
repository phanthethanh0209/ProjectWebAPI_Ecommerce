using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Orders.Commands.CreateOrder;
using ECommerce.Application.Features.Orders.DTOs;
using ECommerce.Application.Features.Orders.Queries.GetOrderById;
using ECommerce.Application.Features.Orders.Queries.GetOrders;
using ECommerce.Application.Features.Orders.Queries.GetUserOrderHistory;
using ECommerce.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost("checkout")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            ResultResponse<Guid> result = await _mediator.Send(command);
            //return result.IsSuccess ?
            //    CreatedAtAction(nameof(GetOrderById), new { id = result.Data }, result.Data) : BadRequest(result.ErrorMessage);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId)
        {
            ResultResponse<OrderDTO> result = await _mediator.Send(new GetOrderByIdQuery(orderId));
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [Authorize]
        [HttpGet("history")]
        public async Task<IActionResult> GetUserOrderHistory([FromQuery] GetUserOrderHistoryQuery query)
        {
            ResultResponse<PagedList<OrderDTO>> result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HasPermission(new[] { "View.Order" })]
        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersQuery query)
        {
            ResultResponse<PagedList<OrderDTO>> result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}
