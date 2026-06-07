namespace RTFleetVehicleService.Application.Features.MaintenanceSchedules.DTOs
{
    public class MaintenanceScheduleDto
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public string Type { get; set; } = string.Empty;
        public int? IntervalKm { get; set; }
        public int? IntervalDays { get; set; }
        public DateTimeOffset? LastServiceAt { get; set; }
        public decimal? LastServiceKm { get; set; }
        public DateTimeOffset? NextDueAt { get; set; }
        public decimal? NextDueKm { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
