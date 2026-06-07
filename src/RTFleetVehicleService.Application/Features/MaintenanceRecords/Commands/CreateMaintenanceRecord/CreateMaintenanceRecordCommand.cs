using MediatR;
using RTFleetVehicleService.Application.Features.MaintenanceRecords.DTOs;

namespace RTFleetVehicleService.Application.Features.MaintenanceRecords.Commands.CreateMaintenanceRecord
{
    public record CreateMaintenanceRecordCommand(
        Guid ScheduleId,
        Guid VehicleId,
        DateTimeOffset PerformedAt,
        decimal OdometerKm,
        string? TechnicianName,
        decimal? CostAmount,
        string? CostCurrency,
        string? Notes
    ) : IRequest<MaintenanceRecordDto>;
}
