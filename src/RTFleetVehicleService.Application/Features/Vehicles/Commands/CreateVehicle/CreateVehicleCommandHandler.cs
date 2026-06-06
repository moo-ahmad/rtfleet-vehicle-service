using MediatR;
using RTFleetVehicleService.Application.Features.Vehicles.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, VehicleDto>
    {
        private readonly IApplicationDbContext _db;

        public CreateVehicleCommandHandler(IApplicationDbContext db) => _db = db;

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
