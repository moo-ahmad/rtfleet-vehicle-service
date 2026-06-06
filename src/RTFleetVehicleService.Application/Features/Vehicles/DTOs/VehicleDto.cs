namespace RTFleetVehicleService.Application.Features.Vehicles.DTOs
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string VIN { get; set; } = string.Empty;
        public string Plate { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Make { get; set; }
        public string? Model { get; set; }
        public short? Year { get; set; }
        public string Status { get; set; } = string.Empty;
        public byte HealthScore { get; set; }
        public decimal OdometerKm { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
