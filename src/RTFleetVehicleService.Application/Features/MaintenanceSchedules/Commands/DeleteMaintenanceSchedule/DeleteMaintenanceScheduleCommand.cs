using MediatR;

namespace RTFleetVehicleService.Application.Features.MaintenanceSchedules.Commands.DeleteMaintenanceSchedule
{
    public record DeleteMaintenanceScheduleCommand(Guid Id) : IRequest<bool>;
}
