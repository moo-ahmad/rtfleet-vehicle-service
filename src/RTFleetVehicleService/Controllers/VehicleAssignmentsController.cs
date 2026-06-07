using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RTFleet.Shared.Common;
using RTFleetVehicleService.Application.Features.VehicleAssignments.Commands.CreateVehicleAssignment;
using RTFleetVehicleService.Application.Features.VehicleAssignments.Commands.DeleteVehicleAssignment;
using RTFleetVehicleService.Application.Features.VehicleAssignments.Commands.UpdateVehicleAssignment;
using RTFleetVehicleService.Application.Features.VehicleAssignments.DTOs;
using RTFleetVehicleService.Application.Features.VehicleAssignments.Queries.GetVehicleAssignment;
using RTFleetVehicleService.Application.Features.VehicleAssignments.Queries.GetVehicleAssignments;

namespace RTFleetVehicleService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleAssignmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleAssignmentsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? vehicleId, [FromQuery] Guid? driverId)
        {
            var result = await _mediator.Send(new GetVehicleAssignmentsQuery(vehicleId, driverId));
            return Ok(APIResponse<IReadOnlyList<VehicleAssignmentDto>>.Success(result));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetVehicleAssignmentQuery(id));
            return Ok(APIResponse<VehicleAssignmentDto>.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleAssignmentCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById),
                new { id = result.Id },
                APIResponse<VehicleAssignmentDto>.Success(result));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVehicleAssignmentCommand command)
        {
            if (id != command.Id)
                return BadRequest(APIResponse<object>.Failure("Route id does not match body id."));

            var result = await _mediator.Send(command);
            return Ok(APIResponse<VehicleAssignmentDto>.Success(result));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteVehicleAssignmentCommand(id));
            return Ok(APIResponse<object>.Success(null, "Vehicle assignment deleted successfully."));
        }
    }
}
