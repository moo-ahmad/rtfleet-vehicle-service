using MediatR;
using RTFleetVehicleService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFleetVehicleService.Application.Features.Drivers.Commands.DeleteDriver
{
    public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, bool>
    {
        private readonly IApplicationDbContext _db;
        public DeleteDriverCommandHandler(IApplicationDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = _db.Drivers.FirstOrDefault(d => d.Id == request.Id && d.TenantId == request.TenantId);
            if (driver == null) return false;

            _db.Drivers.Remove(driver);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
