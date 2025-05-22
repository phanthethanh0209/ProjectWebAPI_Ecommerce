using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Features.Products.Commands.UpdateProduct;
using ECommerce.Application.Features.Products.DTOs;
using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using ECommerce.Application.Features.Products.Queries.GetProductById;
using ECommerce.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] GetAllProductsQuery query)
        {
            ResultResponse<PagedList<GetProductResponse>> result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            ResultResponse<GetProductResponse> result = await _mediator.Send(new GetProductByIdQuery(id));
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HasPermission(new[] { "View.Product" })]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            ResultResponse<Guid> result = await _mediator.Send(command);

            //return CreatedAtAction(nameof(GetProductById), new { id = result.Data }, result.Data); // query new product 
            return result.IsSuccess ?
                CreatedAtAction(nameof(GetProductById), new { id = result.Data }, result.Data) : BadRequest(result.ErrorMessage);
        }

        [HasPermission(new[] { "Update.Product" })]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;

            ResultResponse<Guid> result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HasPermission(new[] { "Delete.Product" })]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            ResultResponse<Unit> result = await _mediator.Send(new DeleteProductCommand(id));
            return result.IsSuccess ? NoContent() : NotFound(result.ErrorMessage);
        }
    }
}
