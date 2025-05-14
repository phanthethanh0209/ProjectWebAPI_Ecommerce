using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Carts.Commands.AddToCart;
using ECommerce.Application.Features.Carts.Commands.ClearCart;
using ECommerce.Application.Features.Carts.Commands.RemoveItem;
using ECommerce.Application.Features.Carts.Commands.UpdateCart;
using ECommerce.Application.Features.Carts.DTOs;
using ECommerce.Application.Features.Carts.Queries.GetCartByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            ResultResponse<Guid> result = await _mediator.Send(command);
            return result.IsSuccess ?
                CreatedAtAction(nameof(GetCartByUserId), result.Data) : BadRequest(result.ErrorMessage);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCartByUserId()
        {
            ResultResponse<CartDTO> result = await _mediator.Send(new GetCartByUserIdQuery());
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartCommand command)
        {
            ResultResponse<Guid> result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("RemoveItem/{productId}")]
        public async Task<IActionResult> RemoveItem([FromRoute] Guid productId)
        {
            ResultResponse<Guid> result = await _mediator.Send(new RemoveItemCommand(productId));
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("ClearCart")]
        public async Task<IActionResult> ClearCart()
        {
            ResultResponse<Unit> result = await _mediator.Send(new ClearCartCommand());
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}
