using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RTFleet.Shared.Common;
using RTFleetVehicleService.Application.Features.MaintenanceSchedules.Commands.CreateMaintenanceSchedule;
using RTFleetVehicleService.Application.Features.MaintenanceSchedules.Commands.DeleteMaintenanceSchedule;
using RTFleetVehicleService.Application.Features.MaintenanceSchedules.Commands.UpdateMaintenanceSchedule;
using RTFleetVehicleService.Application.Features.MaintenanceSchedules.DTOs;
using RTFleetVehicleService.Application.Features.MaintenanceSchedules.Queries.GetMaintenanceSchedule;
using RTFleetVehicleService.Application.Features.MaintenanceSchedules.Queries.GetMaintenanceSchedules;

namespace RTFleetVehicleService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceSchedulesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MaintenanceSchedulesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? vehicleId, [FromQuery] bool? isActive)
        {
            var result = await _mediator.Send(new GetMaintenanceSchedulesQuery(vehicleId, isActive));
            return Ok(APIResponse<IReadOnlyList<MaintenanceScheduleDto>>.Success(result));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetMaintenanceScheduleQuery(id));
            return Ok(APIResponse<MaintenanceScheduleDto>.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMaintenanceScheduleCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById),
                new { id = result.Id },
                APIResponse<MaintenanceScheduleDto>.Success(result));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMaintenanceScheduleCommand command)
        {
            if (id != command.Id)
                return BadRequest(APIResponse<object>.Failure("Route id does not match body id."));

            var result = await _mediator.Send(command);
            return Ok(APIResponse<MaintenanceScheduleDto>.Success(result));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteMaintenanceScheduleCommand(id));
            return Ok(APIResponse<object>.Success(null, "Maintenance schedule deleted successfully."));
        }
    }
}
