namespace RTFleetVehicleService.Application.Features.VehicleGroupMemberships.DTOs
{
    public class VehicleGroupMembershipDto
    {
        public Guid VehicleId { get; set; }
        public Guid GroupId { get; set; }
        public DateTimeOffset AddedAt { get; set; }
    }
}
