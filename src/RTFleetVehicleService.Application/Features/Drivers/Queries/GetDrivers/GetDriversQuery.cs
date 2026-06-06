using MediatR;
using RTFleetVehicleService.Application.Features.Drivers.DTOs;
using RTFleetVehicleService.Application.Features.Vehicles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFleetVehicleService.Application.Features.Drivers.Queries.GerDrivers
{
    public record GetDriversQuery(Guid TenantId) : IRequest<IReadOnlyList<DriverDto>>;
}
