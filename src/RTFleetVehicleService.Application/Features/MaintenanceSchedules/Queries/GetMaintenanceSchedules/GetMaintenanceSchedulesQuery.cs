using MediatR;
using RTFleetVehicleService.Application.Features.MaintenanceSchedules.DTOs;

namespace RTFleetVehicleService.Application.Features.MaintenanceSchedules.Queries.GetMaintenanceSchedules
{
    public record GetMaintenanceSchedulesQuery(Guid? VehicleId, bool? IsActive) : IRequest<IReadOnlyList<MaintenanceScheduleDto>>;
}
