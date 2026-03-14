using System;

namespace Lab3.Models
{
    [Serializable]
    public abstract class LandVehicle : Vehicle
    {
        // Number of wheels
        public int WheelCount { get; set; }
        // Engine type
        public string EngineType { get; set; }

        // Constructor
        public LandVehicle()
        {
            WheelCount = 4;
            EngineType = "Internal Combustion";
        }
    }
}