namespace RTFleetVehicleService.Domain.Entities
{
    public class MaintenanceSchedule
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public string Type { get; set; } = string.Empty;
        public int? IntervalKm { get; set; }
        public int? IntervalDays { get; set; }
        public DateTime? LastServiceAt { get; set; }
        public decimal? LastServiceKm { get; set; }
        public DateTime? NextDueAt { get; set; }
        public decimal? NextDueKm { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}
