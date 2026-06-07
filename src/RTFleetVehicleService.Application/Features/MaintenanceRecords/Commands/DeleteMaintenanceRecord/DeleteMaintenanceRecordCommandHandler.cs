using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Interfaces;

namespace RTFleetVehicleService.Application.Features.MaintenanceRecords.Commands.DeleteMaintenanceRecord
{
    public class DeleteMaintenanceRecordCommandHandler : IRequestHandler<DeleteMaintenanceRecordCommand, bool>
    {
        private readonly IApplicationDbContext _db;

        public DeleteMaintenanceRecordCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteMaintenanceRecordCommand request, CancellationToken cancellationToken)
        {
            var record = await _db.MaintenanceRecords
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Maintenance record {request.Id} not found.");

            _db.MaintenanceRecords.Remove(record);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
