using MediatR;
using RTFleetVehicleService.Application.Features.VehicleGroupMemberships.DTOs;

namespace RTFleetVehicleService.Application.Features.VehicleGroupMemberships.Queries.GetVehicleGroupMemberships
{
    public record GetVehicleGroupMembershipsQuery(Guid? VehicleId, Guid? GroupId) : IRequest<IReadOnlyList<VehicleGroupMembershipDto>>;
}
