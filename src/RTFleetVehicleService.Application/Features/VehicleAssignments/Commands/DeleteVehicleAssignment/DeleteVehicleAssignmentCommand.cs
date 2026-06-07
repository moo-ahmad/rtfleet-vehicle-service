using MediatR;

namespace RTFleetVehicleService.Application.Features.VehicleAssignments.Commands.DeleteVehicleAssignment
{
    public record DeleteVehicleAssignmentCommand(Guid Id) : IRequest<bool>;
}
