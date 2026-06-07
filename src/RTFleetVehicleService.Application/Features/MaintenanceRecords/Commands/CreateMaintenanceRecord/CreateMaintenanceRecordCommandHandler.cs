using MediatR;
using RTFleetVehicleService.Application.Features.MaintenanceRecords.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.MaintenanceRecords.Commands.CreateMaintenanceRecord
{
    public class CreateMaintenanceRecordCommandHandler : IRequestHandler<CreateMaintenanceRecordCommand, MaintenanceRecordDto>
    {
        private readonly IApplicationDbContext _db;

        public CreateMaintenanceRecordCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<MaintenanceRecordDto> Handle(CreateMaintenanceRecordCommand request, CancellationToken cancellationToken)
        {
            var record = new MaintenanceRecord
            {
                Id = Guid.NewGuid(),
                ScheduleId = request.ScheduleId,
                VehicleId = request.VehicleId,
                PerformedAt = request.PerformedAt,
                OdometerKm = request.OdometerKm,
                TechnicianName = request.TechnicianName,
                CostAmount = request.CostAmount,
                CostCurrency = request.CostCurrency ?? "USD",
                Notes = request.Notes,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _db.MaintenanceRecords.Add(record);
            await _db.SaveChangesAsync(cancellationToken);

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
