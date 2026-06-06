using MediatR;
using RTFleetVehicleService.Application.Features.Vehicles.DTOs;

namespace RTFleetVehicleService.Application.Features.Vehicles.Queries.GetVehicle
{
    public record GetVehicleQuery(Guid Id, Guid TenantId) : IRequest<VehicleDto>;
}
