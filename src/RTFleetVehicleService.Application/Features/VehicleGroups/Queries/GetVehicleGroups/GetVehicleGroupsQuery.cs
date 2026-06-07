using MediatR;
using RTFleetVehicleService.Application.Features.VehicleGroups.DTOs;

namespace RTFleetVehicleService.Application.Features.VehicleGroups.Queries.GetVehicleGroups
{
    public record GetVehicleGroupsQuery(Guid TenantId) : IRequest<IReadOnlyList<VehicleGroupDto>>;
}
