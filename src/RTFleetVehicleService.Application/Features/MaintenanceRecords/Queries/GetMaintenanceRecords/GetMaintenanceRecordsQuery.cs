using MediatR;
using RTFleetVehicleService.Application.Features.MaintenanceRecords.DTOs;

namespace RTFleetVehicleService.Application.Features.MaintenanceRecords.Queries.GetMaintenanceRecords
{
    public record GetMaintenanceRecordsQuery(Guid? VehicleId, Guid? ScheduleId) : IRequest<IReadOnlyList<MaintenanceRecordDto>>;
}
