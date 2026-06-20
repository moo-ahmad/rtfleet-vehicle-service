using MediatR;
using RTFleetVehicleService.Application.Features.MaintenanceSchedules.DTOs;

namespace RTFleetVehicleService.Application.Features.MaintenanceSchedules.Commands.UpdateMaintenanceSchedule
{
    public record UpdateMaintenanceScheduleCommand(
        Guid Id,
        string Type,
        int? IntervalKm,
        int? IntervalDays,
        DateTime? LastServiceAt,
        decimal? LastServiceKm,
        DateTime? NextDueAt,
        decimal? NextDueKm,
        bool IsActive
    ) : IRequest<MaintenanceScheduleDto>;
}
