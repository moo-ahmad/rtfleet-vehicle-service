using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Vehicle> Vehicles { get; }
        DbSet<Driver> Drivers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
