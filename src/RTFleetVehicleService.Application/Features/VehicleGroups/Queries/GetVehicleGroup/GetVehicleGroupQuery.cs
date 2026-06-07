using MediatR;
using RTFleetVehicleService.Application.Features.VehicleGroups.DTOs;

namespace RTFleetVehicleService.Application.Features.VehicleGroups.Queries.GetVehicleGroup
{
    public record GetVehicleGroupQuery(Guid Id, Guid TenantId) : IRequest<VehicleGroupDto>;
}
