using System;

namespace Lab3.Models
{
    [Serializable]
    public class Boat : WaterVehicle
    {
        // Number of people
        public int Capacity { get; set; }
        // Cabin presence
        public bool HasCabin { get; set; }

        // Constructor
        public Boat()
        {
            Capacity = 6;
            HasCabin = false;
            Displacement = 500;
        }

        // Returns vehicle type
        public override string GetVehicleType() => "Boat";

        // Returns details string
        public override string GetDetails()
            => $"{Name} - Boat, Capacity: {Capacity} people";
    }
}