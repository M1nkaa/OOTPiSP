using System;

namespace Lab3.Models
{
    [Serializable]
    public abstract class WaterVehicle : Vehicle
    {
        // Weight in tons
        public double Displacement { get; set; }
        // Propulsion type
        public string PropulsionType { get; set; }

        // Constructor
        public WaterVehicle()
        {
            Displacement = 1000;
            PropulsionType = "Motor";
        }
    }
}