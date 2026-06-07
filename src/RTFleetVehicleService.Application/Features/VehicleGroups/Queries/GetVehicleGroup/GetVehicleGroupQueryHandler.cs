using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.VehicleGroups.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.VehicleGroups.Queries.GetVehicleGroup
{
    public class GetVehicleGroupQueryHandler : IRequestHandler<GetVehicleGroupQuery, VehicleGroupDto>
    {
        private readonly IApplicationDbContext _db;

        public GetVehicleGroupQueryHandler(IApplicationDbContext db) => _db = db;

        public async Task<VehicleGroupDto> Handle(GetVehicleGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _db.VehicleGroups
                .FirstOrDefaultAsync(g => g.Id == request.Id && g.TenantId == request.TenantId, cancellationToken)
                ?? throw new KeyNotFoundException($"Vehicle group {request.Id} not found.");

            return MapToDto(group);
        }

        private static VehicleGroupDto MapToDto(VehicleGroup g) => new()
        {
            Id = g.Id,
            TenantId = g.TenantId,
            Name = g.Name,
            Description = g.Description,
            ColourHex = g.ColourHex,
            CreatedAt = g.CreatedAt
        };
    }
}
