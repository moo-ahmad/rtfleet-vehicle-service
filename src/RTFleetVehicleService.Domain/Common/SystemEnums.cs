using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFleetVehicleService.Domain.Common
{
    public class SystemEnums
    {
    }

    public enum VehicleStatus
    {
        Moving,
        Idle,
        Alert,
        Maintenance,
        Unknown
    }

    public enum VehicleType
    {
        BoxTruck,
        CargoVan,
        Flatbed,
        Tanker
    }
}
