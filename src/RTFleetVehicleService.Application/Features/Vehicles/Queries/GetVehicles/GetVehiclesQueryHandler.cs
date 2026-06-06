using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.Vehicles.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.Vehicles.Queries.GetVehicles
{
    public class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, IReadOnlyList<VehicleDto>>
    {
        private readonly IApplicationDbContext _db;

        public GetVehiclesQueryHandler(IApplicationDbContext db) => _db = db;

        public async Task<IReadOnlyList<VehicleDto>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {
            return await _db.Vehicles
                .Where(v => v.TenantId == request.TenantId && !v.IsDeleted)
                .Select(v => new VehicleDto
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
                })
                .ToListAsync(cancellationToken);
        }
    }
}
