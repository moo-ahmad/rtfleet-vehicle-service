using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.MaintenanceRecords.DTOs;
using RTFleetVehicleService.Application.Interfaces;

namespace RTFleetVehicleService.Application.Features.MaintenanceRecords.Queries.GetMaintenanceRecords
{
    public class GetMaintenanceRecordsQueryHandler : IRequestHandler<GetMaintenanceRecordsQuery, IReadOnlyList<MaintenanceRecordDto>>
    {
        private readonly IApplicationDbContext _db;

        public GetMaintenanceRecordsQueryHandler(IApplicationDbContext db) => _db = db;

        public async Task<IReadOnlyList<MaintenanceRecordDto>> Handle(GetMaintenanceRecordsQuery request, CancellationToken cancellationToken)
        {
            var query = _db.MaintenanceRecords.AsQueryable();

            if (request.VehicleId.HasValue)
                query = query.Where(r => r.VehicleId == request.VehicleId.Value);

            if (request.ScheduleId.HasValue)
                query = query.Where(r => r.ScheduleId == request.ScheduleId.Value);

            return await query
                .Select(r => new MaintenanceRecordDto
                {
                    Id = r.Id,
                    ScheduleId = r.ScheduleId,
                    VehicleId = r.VehicleId,
                    PerformedAt = r.PerformedAt,
                    OdometerKm = r.OdometerKm,
                    TechnicianName = r.TechnicianName,
                    CostAmount = r.CostAmount,
                    CostCurrency = r.CostCurrency,
                    Notes = r.Notes,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
