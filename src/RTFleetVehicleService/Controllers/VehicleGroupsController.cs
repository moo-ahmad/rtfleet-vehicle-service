using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RTFleet.Shared.Common;
using RTFleetVehicleService.Application.Features.VehicleGroups.Commands.CreateVehicleGroup;
using RTFleetVehicleService.Application.Features.VehicleGroups.Commands.DeleteVehicleGroup;
using RTFleetVehicleService.Application.Features.VehicleGroups.Commands.UpdateVehicleGroup;
using RTFleetVehicleService.Application.Features.VehicleGroups.DTOs;
using RTFleetVehicleService.Application.Features.VehicleGroups.Queries.GetVehicleGroup;
using RTFleetVehicleService.Application.Features.VehicleGroups.Queries.GetVehicleGroups;

namespace RTFleetVehicleService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleGroupsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid tenantId)
        {
            var result = await _mediator.Send(new GetVehicleGroupsQuery(tenantId));
            return Ok(APIResponse<IReadOnlyList<VehicleGroupDto>>.Success(result));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, [FromQuery] Guid tenantId)
        {
            var result = await _mediator.Send(new GetVehicleGroupQuery(id, tenantId));
            return Ok(APIResponse<VehicleGroupDto>.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleGroupCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById),
                new { id = result.Id, tenantId = result.TenantId },
                APIResponse<VehicleGroupDto>.Success(result));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVehicleGroupCommand command)
        {
            if (id != command.Id)
                return BadRequest(APIResponse<object>.Failure("Route id does not match body id."));

            var result = await _mediator.Send(command);
            return Ok(APIResponse<VehicleGroupDto>.Success(result));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] Guid tenantId)
        {
            await _mediator.Send(new DeleteVehicleGroupCommand(id, tenantId));
            return Ok(APIResponse<object>.Success(null, "Vehicle group deleted successfully."));
        }
    }
}
