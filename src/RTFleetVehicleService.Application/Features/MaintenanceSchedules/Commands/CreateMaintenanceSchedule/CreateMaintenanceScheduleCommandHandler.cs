using MediatR;
using RTFleetVehicleService.Application.Features.MaintenanceSchedules.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.MaintenanceSchedules.Commands.CreateMaintenanceSchedule
{
    public class CreateMaintenanceScheduleCommandHandler : IRequestHandler<CreateMaintenanceScheduleCommand, MaintenanceScheduleDto>
    {
        private readonly IApplicationDbContext _db;

        public CreateMaintenanceScheduleCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<MaintenanceScheduleDto> Handle(CreateMaintenanceScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = new MaintenanceSchedule
            {
                Id = Guid.NewGuid(),
                VehicleId = request.VehicleId,
                Type = request.Type,
                IntervalKm = request.IntervalKm,
                IntervalDays = request.IntervalDays,
                IsActive = true,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _db.MaintenanceSchedules.Add(schedule);
            await _db.SaveChangesAsync(cancellationToken);

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
