using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFleetVehicleService.Application.Features.Drivers.Commands.DeleteDriver
{
    public record DeleteDriverCommand(Guid Id, Guid TenantId) : IRequest<bool>;
}
