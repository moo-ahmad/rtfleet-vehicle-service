using RTFleetVehicleService.API.DTOs;
using RTFleetVehicleService.Application.Features.Auth;
using RTFleetVehicleService.Application.Features.Auth.Commands.Login;
using RTFleetVehicleService.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RTFleetVehicleService.API.Controllers
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(APIResponse<AuthResponseDto>.Success(result));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(APIResponse<AuthResponseDto>.Success(result));
        }
    }
}
