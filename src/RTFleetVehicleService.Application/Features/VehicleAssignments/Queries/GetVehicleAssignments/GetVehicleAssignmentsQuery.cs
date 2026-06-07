using MediatR;
using RTFleetVehicleService.Application.Features.VehicleAssignments.DTOs;

namespace RTFleetVehicleService.Application.Features.VehicleAssignments.Queries.GetVehicleAssignments
{
    public record GetVehicleAssignmentsQuery(Guid? VehicleId, Guid? DriverId) : IRequest<IReadOnlyList<VehicleAssignmentDto>>;
}
