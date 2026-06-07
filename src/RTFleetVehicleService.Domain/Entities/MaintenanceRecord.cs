namespace RTFleetVehicleService.Domain.Entities
{
    public class MaintenanceRecord
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTimeOffset PerformedAt { get; set; }
        public decimal OdometerKm { get; set; }
        public string? TechnicianName { get; set; }
        public decimal? CostAmount { get; set; }
        public string? CostCurrency { get; set; } = "USD";
        public string? Notes { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
