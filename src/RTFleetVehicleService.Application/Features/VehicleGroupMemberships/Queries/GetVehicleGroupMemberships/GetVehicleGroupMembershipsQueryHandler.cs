using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.VehicleGroupMemberships.DTOs;
using RTFleetVehicleService.Application.Interfaces;

namespace RTFleetVehicleService.Application.Features.VehicleGroupMemberships.Queries.GetVehicleGroupMemberships
{
    public class GetVehicleGroupMembershipsQueryHandler : IRequestHandler<GetVehicleGroupMembershipsQuery, IReadOnlyList<VehicleGroupMembershipDto>>
    {
        private readonly IApplicationDbContext _db;

        public GetVehicleGroupMembershipsQueryHandler(IApplicationDbContext db) => _db = db;

        public async Task<IReadOnlyList<VehicleGroupMembershipDto>> Handle(GetVehicleGroupMembershipsQuery request, CancellationToken cancellationToken)
        {
            var query = _db.VehicleGroupMemberships.AsQueryable();

            if (request.VehicleId.HasValue)
                query = query.Where(m => m.VehicleId == request.VehicleId.Value);

            if (request.GroupId.HasValue)
                query = query.Where(m => m.GroupId == request.GroupId.Value);

            return await query
                .Select(m => new VehicleGroupMembershipDto
                {
                    VehicleId = m.VehicleId,
                    GroupId = m.GroupId,
                    AddedAt = m.AddedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
