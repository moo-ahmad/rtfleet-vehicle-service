using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Vehicle> Vehicles { get; }
        DbSet<Driver> Drivers { get; }
        DbSet<VehicleAssignment> VehicleAssignments { get; }
        DbSet<VehicleGroup> VehicleGroups { get; }
        DbSet<VehicleGroupMembership> VehicleGroupMemberships { get; }
        DbSet<MaintenanceSchedule> MaintenanceSchedules { get; }
        DbSet<MaintenanceRecord> MaintenanceRecords { get; }
        DbSet<OutboxMessage> OutboxMessages { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
