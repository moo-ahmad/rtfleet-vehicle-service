using MediatR;
using RTFleetVehicleService.Application.Features.Drivers.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFleetVehicleService.Application.Features.Drivers.Commands.CreateDriver
{
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, DriverDto>
    {
        private readonly IApplicationDbContext _context;

        public CreateDriverCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<DriverDto> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = new Driver
            {
                Id = Guid.NewGuid(),
                TenantId = request.TenantId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                LicenseNumber = request.LicenseNumber,
                LicenseExpiryDate = request.LicenseExpiryDate,
                PhoneNumber = request.PhoneNumber,
                PhotoUrl = request.PhotoUrl,
                CreatedAt = DateTime.UtcNow
            };

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToDto(driver);
        }

        internal static DriverDto MapToDto(Driver d) => new()
        {
            Id = d.Id,
            TenantId = d.TenantId,
            FirstName = d.FirstName,
            LastName = d.LastName,
            LicenseNumber = d.LicenseNumber,
            LicenseExpiryDate = d.LicenseExpiryDate,
            PhoneNumber = d.PhoneNumber,
            PhotoUrl = d.PhotoUrl,
            IsActive = d.IsActive,
            UpdatedAt = d.UpdatedAt,
            CreatedBy = d.CreatedBy
        };
    }
}
