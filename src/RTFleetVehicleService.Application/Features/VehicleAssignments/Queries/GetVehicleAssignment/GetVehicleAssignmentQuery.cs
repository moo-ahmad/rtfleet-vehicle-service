using MediatR;
using RTFleetVehicleService.Application.Features.VehicleAssignments.DTOs;

namespace RTFleetVehicleService.Application.Features.VehicleAssignments.Queries.GetVehicleAssignment
{
    public record GetVehicleAssignmentQuery(Guid Id) : IRequest<VehicleAssignmentDto>;
}
