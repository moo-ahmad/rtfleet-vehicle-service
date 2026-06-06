using MediatR;
using RTFleetVehicleService.Application.Features.Vehicles.DTOs;

namespace RTFleetVehicleService.Application.Features.Vehicles.Commands.CreateVehicle
{
    public record CreateVehicleCommand(
        Guid TenantId,
        string VIN,
        string Plate,
        string Type,
        string? Make,
        string? Model,
        short? Year,
        Guid CreatedBy
    ) : IRequest<VehicleDto>;
}
