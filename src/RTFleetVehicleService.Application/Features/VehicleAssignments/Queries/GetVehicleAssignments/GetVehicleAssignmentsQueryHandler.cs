using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.VehicleAssignments.DTOs;
using RTFleetVehicleService.Application.Interfaces;

namespace RTFleetVehicleService.Application.Features.VehicleAssignments.Queries.GetVehicleAssignments
{
    public class GetVehicleAssignmentsQueryHandler : IRequestHandler<GetVehicleAssignmentsQuery, IReadOnlyList<VehicleAssignmentDto>>
    {
        private readonly IApplicationDbContext _db;

        public GetVehicleAssignmentsQueryHandler(IApplicationDbContext db) => _db = db;

        public async Task<IReadOnlyList<VehicleAssignmentDto>> Handle(GetVehicleAssignmentsQuery request, CancellationToken cancellationToken)
        {
            var query = _db.VehicleAssignments.AsQueryable();

            if (request.VehicleId.HasValue)
                query = query.Where(a => a.VehicleId == request.VehicleId.Value);

            if (request.DriverId.HasValue)
                query = query.Where(a => a.DriverId == request.DriverId.Value);

            return await query
                .Select(a => new VehicleAssignmentDto
                {
                    Id = a.Id,
                    VehicleId = a.VehicleId,
                    DriverId = a.DriverId,
                    AssignedAt = a.AssignedAt,
                    UnassignedAt = a.UnassignedAt,
                    Notes = a.Notes
                })
                .ToListAsync(cancellationToken);
        }
    }
}
