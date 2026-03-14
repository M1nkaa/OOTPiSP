using System;

namespace Lab3.Models
{
    [Serializable]
    public class Motorcycle : LandVehicle
    {
        // Sidecar presence
        public bool HasSidecar { get; set; }
        // Motorcycle style
        public string MotorcycleType { get; set; }

        // Constructor
        public Motorcycle()
        {
            WheelCount = 2;
            MotorcycleType = "Sport";
            HasSidecar = false;
        }

        // Returns vehicle type
        public override string GetVehicleType() => "Motorcycle";

        // Returns details string
        public override string GetDetails()
            => $"{Name} - {MotorcycleType} motorcycle";
    }
}