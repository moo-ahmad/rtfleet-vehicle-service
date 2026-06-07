namespace RTFleetVehicleService.Domain.Entities
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }
        public string EventType { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ProcessedAt { get; set; }
        public byte FailureCount { get; set; }
        public string? LastError { get; set; }
    }
}
