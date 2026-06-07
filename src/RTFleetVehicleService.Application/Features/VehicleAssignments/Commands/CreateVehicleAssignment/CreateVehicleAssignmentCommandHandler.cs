using MediatR;
using RTFleetVehicleService.Application.Features.VehicleAssignments.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.VehicleAssignments.Commands.CreateVehicleAssignment
{
    public class CreateVehicleAssignmentCommandHandler : IRequestHandler<CreateVehicleAssignmentCommand, VehicleAssignmentDto>
    {
        private readonly IApplicationDbContext _db;

        public CreateVehicleAssignmentCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<VehicleAssignmentDto> Handle(CreateVehicleAssignmentCommand request, CancellationToken cancellationToken)
        {
            var assignment = new VehicleAssignment
            {
                Id = Guid.NewGuid(),
                VehicleId = request.VehicleId,
                DriverId = request.DriverId,
                AssignedAt = DateTimeOffset.UtcNow,
                Notes = request.Notes
            };

            _db.VehicleAssignments.Add(assignment);
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
