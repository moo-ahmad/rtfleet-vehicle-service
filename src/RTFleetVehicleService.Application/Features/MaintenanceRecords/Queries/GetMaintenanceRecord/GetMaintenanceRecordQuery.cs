using MediatR;
using RTFleetVehicleService.Application.Features.MaintenanceRecords.DTOs;

namespace RTFleetVehicleService.Application.Features.MaintenanceRecords.Queries.GetMaintenanceRecord
{
    public record GetMaintenanceRecordQuery(Guid Id) : IRequest<MaintenanceRecordDto>;
}
