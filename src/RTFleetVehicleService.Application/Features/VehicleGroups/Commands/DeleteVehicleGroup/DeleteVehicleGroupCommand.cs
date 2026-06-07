using MediatR;

namespace RTFleetVehicleService.Application.Features.VehicleGroups.Commands.DeleteVehicleGroup
{
    public record DeleteVehicleGroupCommand(Guid Id, Guid TenantId) : IRequest<bool>;
}
