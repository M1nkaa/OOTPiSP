using System;

namespace Lab3.Models
{
    [Serializable]
    public class Helicopter : AirVehicle
    {
        // Number of rotor blades
        public int RotorBlades { get; set; }
        // Maximum altitude in meters
        public double MaxAltitude { get; set; }

        // Constructor
        public Helicopter()
        {
            RotorBlades = 4;
            MaxAltitude = 3000;
            Wingspan = 0;
        }

        // Returns vehicle type
        public override string GetVehicleType() => "Helicopter";

        // Returns details string
        public override string GetDetails()
            => $"{Name} - Helicopter, Max altitude: {MaxAltitude}m";
    }
}