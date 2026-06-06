using MediatR;
using Microsoft.AspNetCore.Mvc;
using RTFleetVehicleService.API.DTOs;
using RTFleetVehicleService.Application.Features.Vehicles.Commands.CreateVehicle;
using RTFleetVehicleService.Application.Features.Vehicles.Commands.DeleteVehicle;
using RTFleetVehicleService.Application.Features.Vehicles.Commands.UpdateVehicle;
using RTFleetVehicleService.Application.Features.Vehicles.DTOs;
using RTFleetVehicleService.Application.Features.Vehicles.Queries.GetVehicle;
using RTFleetVehicleService.Application.Features.Vehicles.Queries.GetVehicles;

namespace RTFleetVehicleService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid tenantId)
        {
            var result = await _mediator.Send(new GetVehiclesQuery(tenantId));
            return Ok(APIResponse<IReadOnlyList<VehicleDto>>.Success(result));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, [FromQuery] Guid tenantId)
        {
            var result = await _mediator.Send(new GetVehicleQuery(id, tenantId));
            return Ok(APIResponse<VehicleDto>.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById),
                new { id = result.Id, tenantId = result.TenantId },
                APIResponse<VehicleDto>.Success(result));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVehicleCommand command)
        {
            if (id != command.Id)
                return BadRequest(APIResponse<object>.Failure("Route id does not match body id."));

            var result = await _mediator.Send(command);
            return Ok(APIResponse<VehicleDto>.Success(result));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] Guid tenantId)
        {
            await _mediator.Send(new DeleteVehicleCommand(id, tenantId));
            return Ok(APIResponse<object>.Success(null, "Vehicle deleted successfully."));
        }
    }
}
