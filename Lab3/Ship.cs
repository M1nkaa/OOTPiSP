using System;

namespace Lab3.Models
{
    [Serializable]
    public class Ship : WaterVehicle
    {
        // TEU capacity
        public int ContainerCapacity { get; set; }
        // Ship type
        public string ShipType { get; set; }

        // Constructor
        public Ship()
        {
            ContainerCapacity = 1000;
            ShipType = "Cargo";
            Displacement = 50000;
        }

        // Returns vehicle type
        public override string GetVehicleType() => "Ship";

        // Returns details string
        public override string GetDetails()
            => $"{Name} - {ShipType} ship, Capacity: {ContainerCapacity} TEU";
    }
}