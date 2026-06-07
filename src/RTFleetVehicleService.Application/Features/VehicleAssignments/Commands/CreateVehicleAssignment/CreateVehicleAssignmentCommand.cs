using MediatR;
using RTFleetVehicleService.Application.Features.VehicleAssignments.DTOs;

namespace RTFleetVehicleService.Application.Features.VehicleAssignments.Commands.CreateVehicleAssignment
{
    public record CreateVehicleAssignmentCommand(
        Guid VehicleId,
        Guid DriverId,
        string? Notes
    ) : IRequest<VehicleAssignmentDto>;
}
