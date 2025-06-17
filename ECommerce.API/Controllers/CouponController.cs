using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Coupons.Commands.AddProductToCoupon;
using ECommerce.Application.Features.Coupons.Commands.CreateCoupon;
using ECommerce.Application.Features.Coupons.Commands.RemoveProductToCoupon;
using ECommerce.Application.Features.Coupons.DTOs;
using ECommerce.Application.Features.Coupons.Queries.GetCouponById;
using ECommerce.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CouponController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HasPermission(new[] { "Create.Coupon" })]
        [HttpPost]
        public async Task<IActionResult> CreateCoupon([FromBody] CreateCouponCommand command)
        {
            ResultResponse<Guid> result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HasPermission(new[] { "Create.Coupon" })]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProductToCoupon([FromBody] AddProductToCouponCommand command)
        {
            ResultResponse<Guid> result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateCoupon([FromRoute] Guid id, [FromBody] UpdateCouponCommand command)
        //{
        //    command.Id = id;

        //    ResultResponse<Guid> result = await _mediator.Send(command);
        //    return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoupon([FromRoute] Guid id)
        {
            ResultResponse<CouponResponse> result = await _mediator.Send(new GetCouponByIdQuery(id));
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HasPermission(new[] { "Delete.ProductCoupon" })]
        [HttpDelete("{couponId}/products/{productId}")]
        public async Task<IActionResult> RemoveProductToCoupon([FromRoute] Guid couponId, [FromRoute] Guid productId)
        {
            ResultResponse<Guid> result = await _mediator.Send(new RemoveProductToCouponCommand(couponId, productId));
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }
    }
}
