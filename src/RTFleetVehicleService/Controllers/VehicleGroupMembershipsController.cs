using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RTFleet.Shared.Common;
using RTFleetVehicleService.Application.Features.VehicleGroupMemberships.Commands.CreateVehicleGroupMembership;
using RTFleetVehicleService.Application.Features.VehicleGroupMemberships.Commands.DeleteVehicleGroupMembership;
using RTFleetVehicleService.Application.Features.VehicleGroupMemberships.DTOs;
using RTFleetVehicleService.Application.Features.VehicleGroupMemberships.Queries.GetVehicleGroupMemberships;

namespace RTFleetVehicleService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleGroupMembershipsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleGroupMembershipsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? vehicleId, [FromQuery] Guid? groupId)
        {
            var result = await _mediator.Send(new GetVehicleGroupMembershipsQuery(vehicleId, groupId));
            return Ok(APIResponse<IReadOnlyList<VehicleGroupMembershipDto>>.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleGroupMembershipCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAll),
                new { vehicleId = result.VehicleId, groupId = result.GroupId },
                APIResponse<VehicleGroupMembershipDto>.Success(result));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid vehicleId, [FromQuery] Guid groupId)
        {
            await _mediator.Send(new DeleteVehicleGroupMembershipCommand(vehicleId, groupId));
            return Ok(APIResponse<object>.Success(null, "Vehicle removed from group successfully."));
        }
    }
}
