using MediatR;
using RTFleetVehicleService.Application.Features.Drivers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFleetVehicleService.Application.Features.Drivers.Queries.GerDriver
{
    public record GetDriverQuery(Guid Id, Guid TenantId) : IRequest<DriverDto>;
}
