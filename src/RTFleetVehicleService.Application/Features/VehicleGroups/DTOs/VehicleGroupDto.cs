namespace RTFleetVehicleService.Application.Features.VehicleGroups.DTOs
{
    public class VehicleGroupDto
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string ColourHex { get; set; } = "#2e75b6";
        public DateTimeOffset CreatedAt { get; set; }
    }
}
