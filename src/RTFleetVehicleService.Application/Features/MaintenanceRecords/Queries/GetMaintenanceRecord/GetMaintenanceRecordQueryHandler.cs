using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.MaintenanceRecords.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.MaintenanceRecords.Queries.GetMaintenanceRecord
{
    public class GetMaintenanceRecordQueryHandler : IRequestHandler<GetMaintenanceRecordQuery, MaintenanceRecordDto>
    {
        private readonly IApplicationDbContext _db;

        public GetMaintenanceRecordQueryHandler(IApplicationDbContext db) => _db = db;

        public async Task<MaintenanceRecordDto> Handle(GetMaintenanceRecordQuery request, CancellationToken cancellationToken)
        {
            var record = await _db.MaintenanceRecords
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Maintenance record {request.Id} not found.");

            return MapToDto(record);
        }

        private static MaintenanceRecordDto MapToDto(MaintenanceRecord r) => new()
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
        };
    }
}
