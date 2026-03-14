using System;

namespace Lab3.Models
{
    [Serializable]
    public class Car : LandVehicle
    {
        // Number of doors
        public int DoorsCount { get; set; }
        // Body type
        public string BodyType { get; set; }
        // AC presence
        public bool HasAirConditioning { get; set; }

        // Constructor
        public Car()
        {
            DoorsCount = 4;
            BodyType = "Sedan";
            HasAirConditioning = true;
        }

        // Returns vehicle type
        public override string GetVehicleType() => "Car";

        // Returns details string
        public override string GetDetails()
            => $"{Name} - {BodyType}, {DoorsCount} doors";
    }
}