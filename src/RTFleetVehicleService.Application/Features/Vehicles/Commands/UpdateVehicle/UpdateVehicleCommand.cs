using MediatR;
using RTFleetVehicleService.Application.Features.Vehicles.DTOs;

namespace RTFleetVehicleService.Application.Features.Vehicles.Commands.UpdateVehicle
{
    public record UpdateVehicleCommand(
        Guid Id,
        Guid TenantId,
        string Plate,
        string Type,
        string? Make,
        string? Model,
        short? Year,
        string Status,
        byte HealthScore,
        decimal OdometerKm
    ) : IRequest<VehicleDto>;
}
