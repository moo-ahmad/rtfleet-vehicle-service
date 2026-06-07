using MediatR;
using RTFleetVehicleService.Application.Features.VehicleGroupMemberships.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.VehicleGroupMemberships.Commands.CreateVehicleGroupMembership
{
    public class CreateVehicleGroupMembershipCommandHandler : IRequestHandler<CreateVehicleGroupMembershipCommand, VehicleGroupMembershipDto>
    {
        private readonly IApplicationDbContext _db;

        public CreateVehicleGroupMembershipCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<VehicleGroupMembershipDto> Handle(CreateVehicleGroupMembershipCommand request, CancellationToken cancellationToken)
        {
            var membership = new VehicleGroupMembership
            {
                VehicleId = request.VehicleId,
                GroupId = request.GroupId,
                AddedAt = DateTimeOffset.UtcNow
            };

            _db.VehicleGroupMemberships.Add(membership);
            await _db.SaveChangesAsync(cancellationToken);

            return MapToDto(membership);
        }

        private static VehicleGroupMembershipDto MapToDto(VehicleGroupMembership m) => new()
        {
            VehicleId = m.VehicleId,
            GroupId = m.GroupId,
            AddedAt = m.AddedAt
        };
    }
}
