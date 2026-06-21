using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleet.Shared.Contract.V1.Events;
using RTFleetVehicleService.Application.Features.Vehicles.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, VehicleDto>
    {
        private readonly IApplicationDbContext _db;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateVehicleCommandHandler(IApplicationDbContext db, IPublishEndpoint publishEndpoint)
        {
            _db = db;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<VehicleDto> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _db.Vehicles
                .FirstOrDefaultAsync(v => v.Id == request.Id && v.TenantId == request.TenantId && !v.IsDeleted, cancellationToken)
                ?? throw new KeyNotFoundException($"Vehicle {request.Id} not found.");

            var previousStatus = vehicle.Status;

            vehicle.Plate = request.Plate;
            vehicle.Type = request.Type;
            vehicle.Make = request.Make;
            vehicle.Model = request.Model;
            vehicle.Year = request.Year;
            vehicle.Status = request.Status;
            vehicle.HealthScore = request.HealthScore;
            vehicle.OdometerKm = request.OdometerKm;
            vehicle.UpdatedAt = DateTime.UtcNow;

            if (vehicle.Status != previousStatus)
            {
                await _publishEndpoint.Publish(new VehicleStatusChangedEvent(
                    EventId: Guid.NewGuid(),
                    VehicleId: vehicle.Id,
                    TenantId: vehicle.TenantId,
                    PreviousStatus: previousStatus,
                    NewStatus: vehicle.Status,
                    HealthScore: vehicle.HealthScore,
                    OccurredAt: vehicle.UpdatedAt.Value
                ), cancellationToken);
            }

            // Outbox: event and entity update committed in the same SaveChangesAsync transaction
            await _db.SaveChangesAsync(cancellationToken);

            return MapToDto(vehicle);
        }

        private static VehicleDto MapToDto(Vehicle v) => new()
        {
            Id = v.Id,
            TenantId = v.TenantId,
            VIN = v.VIN,
            Plate = v.Plate,
            Type = v.Type,
            Make = v.Make,
            Model = v.Model,
            Year = v.Year,
            Status = v.Status,
            HealthScore = v.HealthScore,
            OdometerKm = v.OdometerKm,
            CreatedAt = v.CreatedAt,
            UpdatedAt = v.UpdatedAt,
            CreatedBy = v.CreatedBy
        };
    }
}
