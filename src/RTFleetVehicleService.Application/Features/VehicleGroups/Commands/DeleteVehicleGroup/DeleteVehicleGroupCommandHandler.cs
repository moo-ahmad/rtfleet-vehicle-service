using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Interfaces;

namespace RTFleetVehicleService.Application.Features.VehicleGroups.Commands.DeleteVehicleGroup
{
    public class DeleteVehicleGroupCommandHandler : IRequestHandler<DeleteVehicleGroupCommand, bool>
    {
        private readonly IApplicationDbContext _db;

        public DeleteVehicleGroupCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteVehicleGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _db.VehicleGroups
                .FirstOrDefaultAsync(g => g.Id == request.Id && g.TenantId == request.TenantId, cancellationToken)
                ?? throw new KeyNotFoundException($"Vehicle group {request.Id} not found.");

            _db.VehicleGroups.Remove(group);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
