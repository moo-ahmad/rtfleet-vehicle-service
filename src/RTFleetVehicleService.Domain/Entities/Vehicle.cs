using RTFleetVehicleService.Domain.Common;

namespace RTFleetVehicleService.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        public Guid TenantId { get; set; }
        public string VIN { get; set; } = string.Empty;
        public string Plate { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Make { get; set; }
        public string? Model { get; set; }
        public short? Year { get; set; }
        public string Status { get; set; } = "Idle";
        public byte HealthScore { get; set; } = 100;
        public decimal OdometerKm { get; set; } = 0;
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}
