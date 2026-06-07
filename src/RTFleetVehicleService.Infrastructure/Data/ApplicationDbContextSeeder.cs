using Microsoft.EntityFrameworkCore;
using RTFleetVehicleService.Domain.Entities;

namespace RTFleetVehicleService.Infrastructure.Data
{
    public static class ApplicationDbContextSeeder
    {
        private static readonly Guid TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private static readonly Guid AdminUserId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Vehicles.AnyAsync())
                return;

            var now = DateTimeOffset.UtcNow;

            var drivers = new List<Driver>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    TenantId = TenantId,
                    FirstName = "John",
                    LastName = "Smith",
                    LicenseNumber = "DRV-1001",
                    LicenseExpiryDate = DateTime.UtcNow.AddYears(2),
                    PhoneNumber = "+1-555-0101",
                    PhotoUrl = "https://example.com/photos/john-smith.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-200)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    TenantId = TenantId,
                    FirstName = "Maria",
                    LastName = "Garcia",
                    LicenseNumber = "DRV-1002",
                    LicenseExpiryDate = DateTime.UtcNow.AddYears(3),
                    PhoneNumber = "+1-555-0102",
                    PhotoUrl = "https://example.com/photos/maria-garcia.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-150)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    TenantId = TenantId,
                    FirstName = "Ahmed",
                    LastName = "Khan",
                    LicenseNumber = "DRV-1003",
                    LicenseExpiryDate = DateTime.UtcNow.AddYears(1),
                    PhoneNumber = "+1-555-0103",
                    PhotoUrl = "https://example.com/photos/ahmed-khan.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-100)
                }
            };

            var vehicles = new List<Vehicle>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    TenantId = TenantId,
                    VIN = "1HGCM82633A123456",
                    Plate = "ABC-123",
                    Type = "BoxTruck",
                    Make = "Ford",
                    Model = "Transit",
                    Year = 2022,
                    Status = "Idle",
                    HealthScore = 95,
                    OdometerKm = 48500m,
                    CreatedBy = AdminUserId,
                    CreatedAt = DateTime.UtcNow.AddDays(-200)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    TenantId = TenantId,
                    VIN = "2FTRX18W1XCA12345",
                    Plate = "XYZ-789",
                    Type = "CargoVan",
                    Make = "Mercedes-Benz",
                    Model = "Sprinter",
                    Year = 2021,
                    Status = "Moving",
                    HealthScore = 88,
                    OdometerKm = 32100m,
                    CreatedBy = AdminUserId,
                    CreatedAt = DateTime.UtcNow.AddDays(-180)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    TenantId = TenantId,
                    VIN = "3VWFE21C04M123456",
                    Plate = "LMN-456",
                    Type = "Flatbed",
                    Make = "Volvo",
                    Model = "FH16",
                    Year = 2020,
                    Status = "Maintenance",
                    HealthScore = 72,
                    OdometerKm = 121300m,
                    CreatedBy = AdminUserId,
                    CreatedAt = DateTime.UtcNow.AddDays(-400)
                }
            };

            var assignments = new List<VehicleAssignment>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    VehicleId = vehicles[0].Id,
                    DriverId = drivers[0].Id,
                    AssignedAt = now.AddDays(-30),
                    UnassignedAt = null,
                    Notes = "Primary city delivery route"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    VehicleId = vehicles[1].Id,
                    DriverId = drivers[1].Id,
                    AssignedAt = now.AddDays(-10),
                    UnassignedAt = null,
                    Notes = "Regional distribution run"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    VehicleId = vehicles[2].Id,
                    DriverId = drivers[2].Id,
                    AssignedAt = now.AddDays(-60),
                    UnassignedAt = now.AddDays(-5),
                    Notes = "Reassigned after scheduled maintenance"
                }
            };

            var groups = new List<VehicleGroup>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    TenantId = TenantId,
                    Name = "North Region Fleet",
                    Description = "Vehicles operating in the northern delivery zone",
                    ColourHex = "#2e75b6",
                    CreatedAt = now.AddDays(-90)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    TenantId = TenantId,
                    Name = "Heavy Haul Unit",
                    Description = "Flatbeds and tankers used for heavy or oversized loads",
                    ColourHex = "#c0392b",
                    CreatedAt = now.AddDays(-60)
                }
            };

            var memberships = new List<VehicleGroupMembership>
            {
                new() { VehicleId = vehicles[0].Id, GroupId = groups[0].Id, AddedAt = now.AddDays(-85) },
                new() { VehicleId = vehicles[1].Id, GroupId = groups[0].Id, AddedAt = now.AddDays(-80) },
                new() { VehicleId = vehicles[2].Id, GroupId = groups[1].Id, AddedAt = now.AddDays(-55) }
            };

            var schedules = new List<MaintenanceSchedule>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    VehicleId = vehicles[0].Id,
                    Type = "OilChange",
                    IntervalKm = 10000,
                    IntervalDays = 180,
                    LastServiceAt = now.AddDays(-60),
                    LastServiceKm = 45000m,
                    NextDueAt = now.AddDays(120),
                    NextDueKm = 55000m,
                    IsActive = true,
                    CreatedAt = now.AddDays(-200)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    VehicleId = vehicles[1].Id,
                    Type = "TyreRotation",
                    IntervalKm = 20000,
                    IntervalDays = null,
                    LastServiceAt = now.AddDays(-90),
                    LastServiceKm = 30000m,
                    NextDueAt = null,
                    NextDueKm = 50000m,
                    IsActive = true,
                    CreatedAt = now.AddDays(-180)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    VehicleId = vehicles[2].Id,
                    Type = "AnnualService",
                    IntervalKm = null,
                    IntervalDays = 365,
                    LastServiceAt = now.AddDays(-200),
                    LastServiceKm = 120000m,
                    NextDueAt = now.AddDays(165),
                    NextDueKm = null,
                    IsActive = true,
                    CreatedAt = now.AddDays(-400)
                }
            };

            var records = new List<MaintenanceRecord>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    ScheduleId = schedules[0].Id,
                    VehicleId = vehicles[0].Id,
                    PerformedAt = now.AddDays(-60),
                    OdometerKm = 45000m,
                    TechnicianName = "Mike Johnson",
                    CostAmount = 120.50m,
                    CostCurrency = "USD",
                    Notes = "Routine oil and filter change",
                    CreatedAt = now.AddDays(-60)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    ScheduleId = schedules[1].Id,
                    VehicleId = vehicles[1].Id,
                    PerformedAt = now.AddDays(-90),
                    OdometerKm = 30000m,
                    TechnicianName = "Sara Lee",
                    CostAmount = 80.00m,
                    CostCurrency = "USD",
                    Notes = "Rotated all six tyres and checked tread depth",
                    CreatedAt = now.AddDays(-90)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    ScheduleId = schedules[2].Id,
                    VehicleId = vehicles[2].Id,
                    PerformedAt = now.AddDays(-200),
                    OdometerKm = 120000m,
                    TechnicianName = "Carlos Diaz",
                    CostAmount = 450.75m,
                    CostCurrency = "USD",
                    Notes = "Full annual inspection and service",
                    CreatedAt = now.AddDays(-200)
                }
            };

            var outboxMessages = new List<OutboxMessage>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    EventType = "VehicleRegisteredEvent",
                    Payload = "{\"vehicleId\":\"" + vehicles[0].Id + "\",\"vin\":\"" + vehicles[0].VIN + "\",\"tenantId\":\"" + TenantId + "\"}",
                    CreatedAt = now.AddDays(-90),
                    ProcessedAt = now.AddDays(-90).AddMinutes(2),
                    FailureCount = 0,
                    LastError = null
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    EventType = "VehicleAssignmentCreatedEvent",
                    Payload = "{\"assignmentId\":\"" + assignments[1].Id + "\",\"vehicleId\":\"" + vehicles[1].Id + "\",\"driverId\":\"" + drivers[1].Id + "\"}",
                    CreatedAt = now.AddDays(-10),
                    ProcessedAt = null,
                    FailureCount = 0,
                    LastError = null
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    EventType = "MaintenanceScheduleDueEvent",
                    Payload = "{\"scheduleId\":\"" + schedules[2].Id + "\",\"vehicleId\":\"" + vehicles[2].Id + "\",\"type\":\"AnnualService\"}",
                    CreatedAt = now.AddDays(-1),
                    ProcessedAt = null,
                    FailureCount = 1,
                    LastError = "SMTP timeout while notifying maintenance team"
                }
            };

            await context.Drivers.AddRangeAsync(drivers);
            await context.Vehicles.AddRangeAsync(vehicles);
            await context.VehicleAssignments.AddRangeAsync(assignments);
            await context.VehicleGroups.AddRangeAsync(groups);
            await context.VehicleGroupMemberships.AddRangeAsync(memberships);
            await context.MaintenanceSchedules.AddRangeAsync(schedules);
            await context.MaintenanceRecords.AddRangeAsync(records);
            await context.OutboxMessages.AddRangeAsync(outboxMessages);

            await context.SaveChangesAsync();
        }
    }
}
