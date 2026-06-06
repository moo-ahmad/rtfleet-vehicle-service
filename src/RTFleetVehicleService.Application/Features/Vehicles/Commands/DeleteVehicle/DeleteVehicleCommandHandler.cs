using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Interfaces;

namespace RTFleetVehicleService.Application.Features.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, bool>
    {
        private readonly IApplicationDbContext _db;

        public DeleteVehicleCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _db.Vehicles
                .FirstOrDefaultAsync(v => v.Id == request.Id && v.TenantId == request.TenantId && !v.IsDeleted, cancellationToken)
                ?? throw new KeyNotFoundException($"Vehicle {request.Id} not found.");

            vehicle.IsDeleted = true;
            vehicle.DeletedAt = DateTimeOffset.UtcNow;

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
