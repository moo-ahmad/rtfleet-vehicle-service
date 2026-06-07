using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.MaintenanceSchedules.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.MaintenanceSchedules.Queries.GetMaintenanceSchedule
{
    public class GetMaintenanceScheduleQueryHandler : IRequestHandler<GetMaintenanceScheduleQuery, MaintenanceScheduleDto>
    {
        private readonly IApplicationDbContext _db;

        public GetMaintenanceScheduleQueryHandler(IApplicationDbContext db) => _db = db;

        public async Task<MaintenanceScheduleDto> Handle(GetMaintenanceScheduleQuery request, CancellationToken cancellationToken)
        {
            var schedule = await _db.MaintenanceSchedules
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Maintenance schedule {request.Id} not found.");

            return MapToDto(schedule);
        }

        private static MaintenanceScheduleDto MapToDto(MaintenanceSchedule s) => new()
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
        };
    }
}
