using MediatR;

namespace RTFleetVehicleService.Application.Features.MaintenanceRecords.Commands.DeleteMaintenanceRecord
{
    public record DeleteMaintenanceRecordCommand(Guid Id) : IRequest<bool>;
}
