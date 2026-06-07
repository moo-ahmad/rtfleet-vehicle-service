using MediatR;
using RTFleetVehicleService.Application.Features.MaintenanceSchedules.DTOs;

namespace RTFleetVehicleService.Application.Features.MaintenanceSchedules.Queries.GetMaintenanceSchedule
{
    public record GetMaintenanceScheduleQuery(Guid Id) : IRequest<MaintenanceScheduleDto>;
}
