namespace RTFleetVehicleService.Domain.Entities
{
    public class VehicleAssignment
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public Guid DriverId { get; set; }
        public DateTimeOffset AssignedAt { get; set; }
        public DateTimeOffset? UnassignedAt { get; set; }
        public string? Notes { get; set; }
    }
}
