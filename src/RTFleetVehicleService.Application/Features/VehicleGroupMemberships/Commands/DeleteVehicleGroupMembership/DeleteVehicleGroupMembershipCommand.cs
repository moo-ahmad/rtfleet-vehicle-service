using MediatR;

namespace RTFleetVehicleService.Application.Features.VehicleGroupMemberships.Commands.DeleteVehicleGroupMembership
{
    public record DeleteVehicleGroupMembershipCommand(Guid VehicleId, Guid GroupId) : IRequest<bool>;
}
