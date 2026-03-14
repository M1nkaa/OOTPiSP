using System;

namespace Lab3.Models
{
    [Serializable]
    public class Airplane : AirVehicle
    {
        // Number of passengers
        public int PassengerCapacity { get; set; }
        // Max distance in km
        public double FlightRange { get; set; }

        // Constructor
        public Airplane()
        {
            PassengerCapacity = 150;
            FlightRange = 5000;
            EngineCount = 2;
        }

        // Returns vehicle type
        public override string GetVehicleType() => "Airplane";

        // Returns details string
        public override string GetDetails()
            => $"{Name} - Airplane, Capacity: {PassengerCapacity} passengers";
    }
}