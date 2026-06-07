using MediatR;
using RTFleetVehicleService.Application.Features.VehicleAssignments.DTOs;

namespace RTFleetVehicleService.Application.Features.VehicleAssignments.Commands.UpdateVehicleAssignment
{
    public record UpdateVehicleAssignmentCommand(
        Guid Id,
        DateTimeOffset? UnassignedAt,
        string? Notes
    ) : IRequest<VehicleAssignmentDto>;
}
