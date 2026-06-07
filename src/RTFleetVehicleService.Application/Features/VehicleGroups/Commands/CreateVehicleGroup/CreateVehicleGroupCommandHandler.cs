using MediatR;
using RTFleetVehicleService.Application.Features.VehicleGroups.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.VehicleGroups.Commands.CreateVehicleGroup
{
    public class CreateVehicleGroupCommandHandler : IRequestHandler<CreateVehicleGroupCommand, VehicleGroupDto>
    {
        private readonly IApplicationDbContext _db;

        public CreateVehicleGroupCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<VehicleGroupDto> Handle(CreateVehicleGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new VehicleGroup
            {
                Id = Guid.NewGuid(),
                TenantId = request.TenantId,
                Name = request.Name,
                Description = request.Description,
                ColourHex = request.ColourHex ?? "#2e75b6",
                CreatedAt = DateTimeOffset.UtcNow
            };

            _db.VehicleGroups.Add(group);
            await _db.SaveChangesAsync(cancellationToken);

            return MapToDto(group);
        }

        private static VehicleGroupDto MapToDto(VehicleGroup g) => new()
        {
            Id = g.Id,
            TenantId = g.TenantId,
            Name = g.Name,
            Description = g.Description,
            ColourHex = g.ColourHex,
            CreatedAt = g.CreatedAt
        };
    }
}
