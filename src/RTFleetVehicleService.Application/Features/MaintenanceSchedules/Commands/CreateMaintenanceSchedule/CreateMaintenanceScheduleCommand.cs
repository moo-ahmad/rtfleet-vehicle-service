using MediatR;
using RTFleetVehicleService.Application.Features.MaintenanceSchedules.DTOs;

namespace RTFleetVehicleService.Application.Features.MaintenanceSchedules.Commands.CreateMaintenanceSchedule
{
    public record CreateMaintenanceScheduleCommand(
        Guid VehicleId,
        string Type,
        int? IntervalKm,
        int? IntervalDays
    ) : IRequest<MaintenanceScheduleDto>;
}
