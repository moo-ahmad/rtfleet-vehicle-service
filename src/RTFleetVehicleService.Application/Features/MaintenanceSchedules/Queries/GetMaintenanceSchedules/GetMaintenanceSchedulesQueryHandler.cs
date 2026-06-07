using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.MaintenanceSchedules.DTOs;
using RTFleetVehicleService.Application.Interfaces;

namespace RTFleetVehicleService.Application.Features.MaintenanceSchedules.Queries.GetMaintenanceSchedules
{
    public class GetMaintenanceSchedulesQueryHandler : IRequestHandler<GetMaintenanceSchedulesQuery, IReadOnlyList<MaintenanceScheduleDto>>
    {
        private readonly IApplicationDbContext _db;

        public GetMaintenanceSchedulesQueryHandler(IApplicationDbContext db) => _db = db;

        public async Task<IReadOnlyList<MaintenanceScheduleDto>> Handle(GetMaintenanceSchedulesQuery request, CancellationToken cancellationToken)
        {
            var query = _db.MaintenanceSchedules.AsQueryable();

            if (request.VehicleId.HasValue)
                query = query.Where(s => s.VehicleId == request.VehicleId.Value);

            if (request.IsActive.HasValue)
                query = query.Where(s => s.IsActive == request.IsActive.Value);

            return await query
                .Select(s => new MaintenanceScheduleDto
                {
                    Id = s.Id,
                    VehicleId = s.VehicleId,
                    Type = s.Type,
                    IntervalKm = s.IntervalKm,
                    IntervalDays = s.IntervalDays,
                    LastServiceAt = s.LastServiceAt,
                    LastServiceKm = s.LastServiceKm,
                    NextDueAt = s.NextDueAt,
                    NextDueKm = s.NextDueKm,
                    IsActive = s.IsActive,
                    CreatedAt = s.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
