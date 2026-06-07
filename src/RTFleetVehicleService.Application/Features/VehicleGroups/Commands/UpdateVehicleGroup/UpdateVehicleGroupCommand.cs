using MediatR;
using RTFleetVehicleService.Application.Features.VehicleGroups.DTOs;

namespace RTFleetVehicleService.Application.Features.VehicleGroups.Commands.UpdateVehicleGroup
{
    public record UpdateVehicleGroupCommand(
        Guid Id,
        Guid TenantId,
        string Name,
        string? Description,
        string ColourHex
    ) : IRequest<VehicleGroupDto>;
}
