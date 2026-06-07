using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<VehicleAssignment> VehicleAssignments { get; set; }
        public DbSet<VehicleGroup> VehicleGroups { get; set; }
        public DbSet<VehicleGroupMembership> VehicleGroupMemberships { get; set; }
        public DbSet<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Vehicle>(e =>
            {
                e.ToTable("Vehicles");
                e.HasKey(v => v.Id);
                e.Property(v => v.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                e.Property(v => v.VIN).HasMaxLength(17).IsRequired();
                e.Property(v => v.Plate).HasMaxLength(20).IsRequired();
                e.Property(v => v.Type).HasMaxLength(50).IsRequired();
                e.Property(v => v.Make).HasMaxLength(100);
                e.Property(v => v.Model).HasMaxLength(100);
                e.Property(v => v.Status).HasMaxLength(30).IsRequired().HasDefaultValue("Idle");
                e.Property(v => v.HealthScore).IsRequired().HasDefaultValue((byte)100);
                e.Property(v => v.OdometerKm).HasColumnType("decimal(10,2)").IsRequired().HasDefaultValue(0m);
                e.Property(v => v.RowVersion).IsRowVersion();
                e.HasIndex(v => new { v.TenantId, v.VIN }).IsUnique().HasDatabaseName("UQ_Vehicles_TenantVIN");
                e.HasIndex(v => new { v.TenantId, v.Plate }).IsUnique().HasDatabaseName("UQ_Vehicles_TenantPlate");
                e.HasIndex(v => v.TenantId).HasFilter("[IsDeleted] = 0").HasDatabaseName("IX_Vehicles_TenantId");
                e.HasIndex(v => new { v.TenantId, v.Status }).HasFilter("[IsDeleted] = 0").HasDatabaseName("IX_Vehicles_Status");
            });

            builder.Entity<Driver>(e =>
            {
                e.ToTable("Drivers");
                e.HasKey(d => d.Id);
                e.Property(d => d.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                e.Property(d => d.FirstName).HasMaxLength(100).IsRequired();
                e.Property(d => d.LastName).HasMaxLength(100).IsRequired();
                e.Property(d => d.LicenseNumber).HasMaxLength(50).IsRequired();
                e.Property(d => d.PhoneNumber).HasMaxLength(20);
                e.Property(d => d.PhotoUrl).HasMaxLength(1000);
                e.HasIndex(d => new { d.TenantId, d.LicenseNumber}).IsUnique().HasDatabaseName("UQ_Drivers_TenantLicense");
            });

            builder.Entity<VehicleAssignment>(e =>
            {
                e.ToTable("VehicleAssignments");
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                e.Property(a => a.AssignedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
                e.Property(a => a.Notes).HasMaxLength(500);
                e.HasOne<Vehicle>().WithMany().HasForeignKey(a => a.VehicleId);
                e.HasOne<Driver>().WithMany().HasForeignKey(a => a.DriverId);
                e.HasIndex(a => a.VehicleId).HasFilter("[UnassignedAt] IS NULL").HasDatabaseName("IX_VehicleAssignments_Vehicle");
            });

            builder.Entity<VehicleGroup>(e =>
            {
                e.ToTable("VehicleGroups");
                e.HasKey(g => g.Id);
                e.Property(g => g.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                e.Property(g => g.Name).HasMaxLength(200).IsRequired();
                e.Property(g => g.Description).HasMaxLength(500);
                e.Property(g => g.ColourHex).HasMaxLength(7).IsFixedLength().IsRequired().HasDefaultValue("#2e75b6");
                e.Property(g => g.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
            });

            builder.Entity<VehicleGroupMembership>(e =>
            {
                e.ToTable("VehicleGroupMemberships");
                e.HasKey(m => new { m.VehicleId, m.GroupId });
                e.Property(m => m.AddedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
                e.HasOne<Vehicle>().WithMany().HasForeignKey(m => m.VehicleId);
                e.HasOne<VehicleGroup>().WithMany().HasForeignKey(m => m.GroupId);
            });

            builder.Entity<MaintenanceSchedule>(e =>
            {
                e.ToTable("MaintenanceSchedules");
                e.HasKey(s => s.Id);
                e.Property(s => s.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                e.Property(s => s.Type).HasMaxLength(100).IsRequired();
                e.Property(s => s.LastServiceKm).HasColumnType("decimal(10,2)");
                e.Property(s => s.NextDueKm).HasColumnType("decimal(10,2)");
                e.Property(s => s.IsActive).IsRequired().HasDefaultValue(true);
                e.Property(s => s.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
                e.HasOne<Vehicle>().WithMany().HasForeignKey(s => s.VehicleId);
                e.HasIndex(s => s.NextDueAt).HasFilter("[IsActive] = 1").HasDatabaseName("IX_MaintenanceSchedules_Due");
            });

            builder.Entity<MaintenanceRecord>(e =>
            {
                e.ToTable("MaintenanceRecords");
                e.HasKey(r => r.Id);
                e.Property(r => r.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                e.Property(r => r.OdometerKm).HasColumnType("decimal(10,2)").IsRequired();
                e.Property(r => r.TechnicianName).HasMaxLength(200);
                e.Property(r => r.CostAmount).HasColumnType("decimal(10,2)");
                e.Property(r => r.CostCurrency).HasMaxLength(3).IsFixedLength().HasDefaultValue("USD");
                e.Property(r => r.Notes).HasMaxLength(1000);
                e.Property(r => r.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
                e.HasOne<MaintenanceSchedule>().WithMany().HasForeignKey(r => r.ScheduleId);
                e.HasOne<Vehicle>().WithMany().HasForeignKey(r => r.VehicleId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<OutboxMessage>(e =>
            {
                e.ToTable("OutboxMessages");
                e.HasKey(m => m.Id);
                e.Property(m => m.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                e.Property(m => m.EventType).HasMaxLength(200).IsRequired();
                e.Property(m => m.Payload).IsRequired();
                e.Property(m => m.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
                e.Property(m => m.FailureCount).IsRequired().HasDefaultValue((byte)0);
                e.Property(m => m.LastError).HasMaxLength(500);
                e.HasIndex(m => m.CreatedAt).HasFilter("[ProcessedAt] IS NULL").HasDatabaseName("IX_OutboxMessages_Unprocessed");
            });
        }
    }
}
