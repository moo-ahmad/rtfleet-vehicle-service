using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Interfaces;

namespace RTFleetVehicleService.Application.Features.VehicleAssignments.Commands.DeleteVehicleAssignment
{
    public class DeleteVehicleAssignmentCommandHandler : IRequestHandler<DeleteVehicleAssignmentCommand, bool>
    {
        private readonly IApplicationDbContext _db;

        public DeleteVehicleAssignmentCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteVehicleAssignmentCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _db.VehicleAssignments
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Vehicle assignment {request.Id} not found.");

            _db.VehicleAssignments.Remove(assignment);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
