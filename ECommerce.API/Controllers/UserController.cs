using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Users.Commands.DeleteUser;
using ECommerce.Application.Features.Users.Commands.Register;
using ECommerce.Application.Features.Users.Commands.UpdateUser;
using ECommerce.Application.Features.Users.DTOs;
using ECommerce.Application.Features.Users.Queries.GetAllUser;
using ECommerce.Application.Features.Users.Queries.GetUserById;
using ECommerce.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterCommand command)
        {
            ResultResponse<Guid> result = await _mediator.Send(command);
            return result.IsSuccess ? CreatedAtAction(nameof(GetUserById), new { id = result.Data }, result.Data) : BadRequest(result.ErrorMessage);
        }

        //[HasPermission(new[] { "Update.User" })]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;

            ResultResponse<Guid> result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HasPermission(new[] { "Delete.User" })]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            ResultResponse<Unit> result = await _mediator.Send(new DeleteUserCommand(id));
            return result.IsSuccess ? NoContent() : NotFound(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            ResultResponse<GetUserResponse> result = await _mediator.Send(new GetUserByIdQuery(id));
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HasPermission(new[] { "View.User" })]
        [HttpGet]
        public async Task<IActionResult> GetAllUser([FromQuery] GetAllUserQuery query)
        {
            ResultResponse<PagedList<GetUserResponse>> result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }
    }
}
