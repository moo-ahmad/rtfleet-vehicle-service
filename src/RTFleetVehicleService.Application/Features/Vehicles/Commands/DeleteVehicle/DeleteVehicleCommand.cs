using MediatR;

namespace RTFleetVehicleService.Application.Features.Vehicles.Commands.DeleteVehicle
{
    public record DeleteVehicleCommand(Guid Id, Guid TenantId) : IRequest<bool>;
}
