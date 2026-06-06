using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RTFleet.Shared.Common;
using RTFleetVehicleService.Application.Features.Drivers.Commands.CreateDriver;
using RTFleetVehicleService.Application.Features.Drivers.Commands.DeleteDriver;
using RTFleetVehicleService.Application.Features.Drivers.Commands.UpdateDriver;
using RTFleetVehicleService.Application.Features.Drivers.DTOs;
using RTFleetVehicleService.Application.Features.Drivers.Queries.GerDriver;
using RTFleetVehicleService.Application.Features.Drivers.Queries.GerDrivers;

namespace RTFleetVehicleService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DriversController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid tenantId)
        {
            var query = new GetDriversQuery(tenantId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, [FromQuery] Guid tenantId)
        {
            var result = await _mediator.Send(new GetDriverQuery(id, tenantId));
            return Ok(APIResponse<DriverDto>.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDriverCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById),
                new { id = result.Id, tenantId = result.TenantId },
                APIResponse<DriverDto>.Success(result));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDriverCommand command)
        {
            if (id != command.Id)
                return BadRequest(APIResponse<object>.Failure("Route id does not match body id."));

            var result = await _mediator.Send(command);
            return Ok(APIResponse<DriverDto>.Success(result));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] Guid tenantId)
        {
            await _mediator.Send(new DeleteDriverCommand(id, tenantId));
            return Ok(APIResponse<object>.Success(null, "Vehicle deleted successfully."));
        }
    }
}
