using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.VehicleAssignments.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.VehicleAssignments.Commands.UpdateVehicleAssignment
{
    public class UpdateVehicleAssignmentCommandHandler : IRequestHandler<UpdateVehicleAssignmentCommand, VehicleAssignmentDto>
    {
        private readonly IApplicationDbContext _db;

        public UpdateVehicleAssignmentCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<VehicleAssignmentDto> Handle(UpdateVehicleAssignmentCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _db.VehicleAssignments
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Vehicle assignment {request.Id} not found.");

            assignment.UnassignedAt = request.UnassignedAt;
            assignment.Notes = request.Notes;

            await _db.SaveChangesAsync(cancellationToken);

            return MapToDto(assignment);
        }

        private static VehicleAssignmentDto MapToDto(VehicleAssignment a) => new()
        {
            Id = a.Id,
            VehicleId = a.VehicleId,
            DriverId = a.DriverId,
            AssignedAt = a.AssignedAt,
            UnassignedAt = a.UnassignedAt,
            Notes = a.Notes
        };
    }
}
