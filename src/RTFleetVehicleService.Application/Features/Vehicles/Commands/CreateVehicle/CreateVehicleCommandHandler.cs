    using MassTransit;
using MediatR;
using RTFleet.Shared.Contract.V1.Events;
using RTFleetVehicleService.Application.Features.Vehicles.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, VehicleDto>
    {
        private readonly IApplicationDbContext _db;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateVehicleCommandHandler(IApplicationDbContext db, IPublishEndpoint publishEndpoint)
        {
            _db = db;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<VehicleDto> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                TenantId = request.TenantId,
                VIN = request.VIN,
                Plate = request.Plate,
                Type = request.Type,
                Make = request.Make,
                Model = request.Model,
                Year = request.Year,
                Status = "Idle",
                HealthScore = 100,
                OdometerKm = 0,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            _db.Vehicles.Add(vehicle);

            await _publishEndpoint.Publish(new VehicleRegisteredEvent(
                EventId: Guid.NewGuid(),
                VehicleId: vehicle.Id,
                TenantId: vehicle.TenantId,
                VIN: vehicle.VIN,
                Plate: vehicle.Plate,
                Type: vehicle.Type,
                OccurredAt: vehicle.CreatedAt
            ), cancellationToken);

            // Outbox: event and entity write committed in the same SaveChangesAsync transaction
            await _db.SaveChangesAsync(cancellationToken);

            return MapToDto(vehicle);
        }

        internal static VehicleDto MapToDto(Vehicle v) => new()
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
