using MediatR;
using RTFleetVehicleService.Application.Features.VehicleGroupMemberships.DTOs;

namespace RTFleetVehicleService.Application.Features.VehicleGroupMemberships.Commands.CreateVehicleGroupMembership
{
    public record CreateVehicleGroupMembershipCommand(
        Guid VehicleId,
        Guid GroupId
    ) : IRequest<VehicleGroupMembershipDto>;
}
