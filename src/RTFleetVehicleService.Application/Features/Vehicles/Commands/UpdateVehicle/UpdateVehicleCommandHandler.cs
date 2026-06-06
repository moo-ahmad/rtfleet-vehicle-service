using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.Vehicles.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, VehicleDto>
    {
        private readonly IApplicationDbContext _db;

        public UpdateVehicleCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<VehicleDto> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _db.Vehicles
                .FirstOrDefaultAsync(v => v.Id == request.Id && v.TenantId == request.TenantId && !v.IsDeleted, cancellationToken)
                ?? throw new KeyNotFoundException($"Vehicle {request.Id} not found.");

            vehicle.Plate = request.Plate;
            vehicle.Type = request.Type;
            vehicle.Make = request.Make;
            vehicle.Model = request.Model;
            vehicle.Year = request.Year;
            vehicle.Status = request.Status;
            vehicle.HealthScore = request.HealthScore;
            vehicle.OdometerKm = request.OdometerKm;
            vehicle.UpdatedAt = DateTime.UtcNow;

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
