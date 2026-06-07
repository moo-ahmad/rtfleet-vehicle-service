using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Interfaces;

namespace RTFleetVehicleService.Application.Features.VehicleGroupMemberships.Commands.DeleteVehicleGroupMembership
{
    public class DeleteVehicleGroupMembershipCommandHandler : IRequestHandler<DeleteVehicleGroupMembershipCommand, bool>
    {
        private readonly IApplicationDbContext _db;

        public DeleteVehicleGroupMembershipCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteVehicleGroupMembershipCommand request, CancellationToken cancellationToken)
        {
            var membership = await _db.VehicleGroupMemberships
                .FirstOrDefaultAsync(m => m.VehicleId == request.VehicleId && m.GroupId == request.GroupId, cancellationToken)
                ?? throw new KeyNotFoundException($"Membership of vehicle {request.VehicleId} in group {request.GroupId} not found.");

            _db.VehicleGroupMemberships.Remove(membership);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
