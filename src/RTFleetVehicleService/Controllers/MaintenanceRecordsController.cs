using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RTFleet.Shared.Common;
using RTFleetVehicleService.Application.Features.MaintenanceRecords.Commands.CreateMaintenanceRecord;
using RTFleetVehicleService.Application.Features.MaintenanceRecords.Commands.DeleteMaintenanceRecord;
using RTFleetVehicleService.Application.Features.MaintenanceRecords.Commands.UpdateMaintenanceRecord;
using RTFleetVehicleService.Application.Features.MaintenanceRecords.DTOs;
using RTFleetVehicleService.Application.Features.MaintenanceRecords.Queries.GetMaintenanceRecord;
using RTFleetVehicleService.Application.Features.MaintenanceRecords.Queries.GetMaintenanceRecords;

namespace RTFleetVehicleService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceRecordsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MaintenanceRecordsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? vehicleId, [FromQuery] Guid? scheduleId)
        {
            var result = await _mediator.Send(new GetMaintenanceRecordsQuery(vehicleId, scheduleId));
            return Ok(APIResponse<IReadOnlyList<MaintenanceRecordDto>>.Success(result));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetMaintenanceRecordQuery(id));
            return Ok(APIResponse<MaintenanceRecordDto>.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMaintenanceRecordCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById),
                new { id = result.Id },
                APIResponse<MaintenanceRecordDto>.Success(result));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMaintenanceRecordCommand command)
        {
            if (id != command.Id)
                return BadRequest(APIResponse<object>.Failure("Route id does not match body id."));

            var result = await _mediator.Send(command);
            return Ok(APIResponse<MaintenanceRecordDto>.Success(result));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteMaintenanceRecordCommand(id));
            return Ok(APIResponse<object>.Success(null, "Maintenance record deleted successfully."));
        }
    }
}
