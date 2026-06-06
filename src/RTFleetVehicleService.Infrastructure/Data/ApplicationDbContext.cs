using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }

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
        }
    }
}
