using System;

namespace Lab3.Models
{
    [Serializable]
    public abstract class AirVehicle : Vehicle
    {
        // Wingspan in meters
        public double Wingspan { get; set; }
        // Number of engines
        public int EngineCount { get; set; }

        // Constructor
        public AirVehicle()
        {
            Wingspan = 10;
            EngineCount = 2;
        }
    }
}