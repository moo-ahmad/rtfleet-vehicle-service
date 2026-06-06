using MediatR;
using RTFleetVehicleService.Application.Features.Vehicles.DTOs;

namespace RTFleetVehicleService.Application.Features.Vehicles.Queries.GetVehicles
{
    public record GetVehiclesQuery(Guid TenantId) : IRequest<IReadOnlyList<VehicleDto>>;
}
