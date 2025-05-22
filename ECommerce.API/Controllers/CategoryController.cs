using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Categories.Commands.CreateCategory;
using ECommerce.Application.Features.Categories.Commands.DeleteCategory;
using ECommerce.Application.Features.Categories.Commands.UpdateCategory;
using ECommerce.Application.Features.Categories.DTOs;
using ECommerce.Application.Features.Categories.Queries.GetAllCategory;
using ECommerce.Application.Features.Categories.Queries.GetCategoryById;
using ECommerce.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory([FromQuery] GetAllCategoryQuery query)
        {
            ResultResponse<PagedList<GetCategoryResponse>> result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] GetCategoryByIdQuery query)
        {
            ResultResponse<GetCategoryResponse> result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HasPermission(new[] { "Create.Category" })]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            ResultResponse<Guid> result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HasPermission(new[] { "Update.Category" })]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryCommand command)
        {
            command.Id = id;

            ResultResponse<Guid> result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HasPermission(new[] { "Delete.Category" })]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] DeleteCategoryCommand command)
        {
            ResultResponse<Unit> result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}
