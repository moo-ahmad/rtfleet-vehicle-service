using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.VehicleGroups.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Features.VehicleGroups.Commands.UpdateVehicleGroup
{
    public class UpdateVehicleGroupCommandHandler : IRequestHandler<UpdateVehicleGroupCommand, VehicleGroupDto>
    {
        private readonly IApplicationDbContext _db;

        public UpdateVehicleGroupCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<VehicleGroupDto> Handle(UpdateVehicleGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _db.VehicleGroups
                .FirstOrDefaultAsync(g => g.Id == request.Id && g.TenantId == request.TenantId, cancellationToken)
                ?? throw new KeyNotFoundException($"Vehicle group {request.Id} not found.");

            group.Name = request.Name;
            group.Description = request.Description;
            group.ColourHex = request.ColourHex;

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
