namespace RTFleetVehicleService.Application.Features.VehicleAssignments.DTOs
{
    public class VehicleAssignmentDto
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public Guid DriverId { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime? UnassignedAt { get; set; }
        public string? Notes { get; set; }
    }
}
