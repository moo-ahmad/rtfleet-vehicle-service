using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.VehicleGroups.DTOs;
using RTFleetVehicleService.Application.Interfaces;

namespace RTFleetVehicleService.Application.Features.VehicleGroups.Queries.GetVehicleGroups
{
    public class GetVehicleGroupsQueryHandler : IRequestHandler<GetVehicleGroupsQuery, IReadOnlyList<VehicleGroupDto>>
    {
        private readonly IApplicationDbContext _db;

        public GetVehicleGroupsQueryHandler(IApplicationDbContext db) => _db = db;

        public async Task<IReadOnlyList<VehicleGroupDto>> Handle(GetVehicleGroupsQuery request, CancellationToken cancellationToken)
        {
            return await _db.VehicleGroups
                .Where(g => g.TenantId == request.TenantId)
                .Select(g => new VehicleGroupDto
                {
                    Id = g.Id,
                    TenantId = g.TenantId,
                    Name = g.Name,
                    Description = g.Description,
                    ColourHex = g.ColourHex,
                    CreatedAt = g.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
