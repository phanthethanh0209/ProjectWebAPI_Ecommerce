using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Authentication.Commands.Login;
using ECommerce.Application.Features.Authentication.Commands.Refresh;
using ECommerce.Application.Features.Authentication.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            ResultResponse<LoginResponse> result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            ResultResponse<LoginResponse> result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

    }
}
