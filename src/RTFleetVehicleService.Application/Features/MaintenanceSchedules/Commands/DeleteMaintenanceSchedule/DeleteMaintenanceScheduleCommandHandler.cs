using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Interfaces;

namespace RTFleetVehicleService.Application.Features.MaintenanceSchedules.Commands.DeleteMaintenanceSchedule
{
    public class DeleteMaintenanceScheduleCommandHandler : IRequestHandler<DeleteMaintenanceScheduleCommand, bool>
    {
        private readonly IApplicationDbContext _db;

        public DeleteMaintenanceScheduleCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteMaintenanceScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _db.MaintenanceSchedules
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Maintenance schedule {request.Id} not found.");

            _db.MaintenanceSchedules.Remove(schedule);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
