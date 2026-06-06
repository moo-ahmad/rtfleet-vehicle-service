using MediatR;
using RTFleetVehicleService.Application.Features.Drivers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFleetVehicleService.Application.Features.Drivers.Commands.UpdateDriver
{
    public record UpdateDriverCommand(
         Guid Id,
         Guid TenantId,
         string FirstName,
         string LastName,
         string LicenseNumber,
         DateTime LicenseExpiryDate,
         string PhoneNumber,
         string PhotoUrl) : IRequest<DriverDto>;
}
