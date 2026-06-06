using MediatR;
using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Features.Drivers.DTOs;
using RTFleetVehicleService.Application.Features.Drivers.Queries.GerDrivers;
using RTFleetVehicleService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFleetVehicleService.Application.Features.Drivers.Queries.GetDrivers
{
    public class GetDriversQueryHandler : IRequestHandler<GetDriversQuery, IReadOnlyList<DriverDto>>
    {
        private readonly IApplicationDbContext _db;

        public GetDriversQueryHandler(IApplicationDbContext db) => _db = db;
        public async Task<IReadOnlyList<DriverDto>> Handle(GetDriversQuery request, CancellationToken cancellationToken)
        {
            return await _db.Drivers
                .Where(d => d.TenantId == request.TenantId && !d.IsDeleted)
                .Select(d => new DriverDto
                {
                    Id = d.Id,
                    TenantId = d.TenantId,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    LicenseNumber = d.LicenseNumber,
                    LicenseExpiryDate = d.LicenseExpiryDate,
                    PhoneNumber = d.PhoneNumber,
                    PhotoUrl = d.PhotoUrl
                })
                .ToListAsync(cancellationToken);
        }
    }
}
