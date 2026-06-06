using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Application.Interfaces;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

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
        }
    }
}
