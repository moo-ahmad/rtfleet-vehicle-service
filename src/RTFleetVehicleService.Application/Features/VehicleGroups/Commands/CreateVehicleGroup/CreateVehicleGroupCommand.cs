using MediatR;
using RTFleetVehicleService.Application.Features.VehicleGroups.DTOs;

namespace RTFleetVehicleService.Application.Features.VehicleGroups.Commands.CreateVehicleGroup
{
    public record CreateVehicleGroupCommand(
        Guid TenantId,
        string Name,
        string? Description,
        string? ColourHex
    ) : IRequest<VehicleGroupDto>;
}
