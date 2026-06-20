namespace RTFleetVehicleService.Domain.Entities
{
    public class VehicleGroupMembership
    {
        public Guid VehicleId { get; set; }
        public Guid GroupId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
