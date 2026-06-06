using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.Vehicles.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.Vehicles.Queries.GetVehicle
{
    public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, VehicleDto>
    {
        private readonly IApplicationDbContext _db;

        public GetVehicleQueryHandler(IApplicationDbContext db) => _db = db;

        public async Task<VehicleDto> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _db.Vehicles
                .FirstOrDefaultAsync(v => v.Id == request.Id && v.TenantId == request.TenantId && !v.IsDeleted, cancellationToken)
                ?? throw new KeyNotFoundException($"Vehicle {request.Id} not found.");

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
