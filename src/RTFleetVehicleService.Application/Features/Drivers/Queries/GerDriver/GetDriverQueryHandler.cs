using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.Drivers.DTOs;
using RTFleetVehicleService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFleetVehicleService.Application.Features.Drivers.Queries.GerDriver
{
    public class GetDriverQueryHandler : IRequestHandler<GetDriverQuery, DriverDto>
    {
        private readonly IApplicationDbContext _db;

        public GetDriverQueryHandler(IApplicationDbContext db) => _db = db;

        public async Task<DriverDto> Handle(GetDriverQuery request, CancellationToken cancellationToken)
        {
            var driver = await _db.Drivers
                .FirstOrDefaultAsync(v => v.Id == request.Id && v.TenantId == request.TenantId && !v.IsDeleted, cancellationToken)
                ?? throw new KeyNotFoundException($"Vehicle {request.Id} not found.");


            return new DriverDto
            {
                Id = driver.Id,
                TenantId = driver.TenantId,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                LicenseNumber = driver.LicenseNumber,
                LicenseExpiryDate = driver.LicenseExpiryDate,
                PhoneNumber = driver.PhoneNumber,
                PhotoUrl = driver.PhotoUrl
            };
        }
    }
}
