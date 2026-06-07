using MediatR;
using RTFleetVehicleService.Application.Features.MaintenanceRecords.DTOs;

namespace RTFleetVehicleService.Application.Features.MaintenanceRecords.Commands.UpdateMaintenanceRecord
{
    public record UpdateMaintenanceRecordCommand(
        Guid Id,
        DateTimeOffset PerformedAt,
        decimal OdometerKm,
        string? TechnicianName,
        decimal? CostAmount,
        string? CostCurrency,
        string? Notes
    ) : IRequest<MaintenanceRecordDto>;
}
