using System;

namespace Lab3
{
    [Serializable]
    public abstract class Vehicle
    {
        // Vehicle name
        public string Name { get; set; }
        // Manufacture year
        public int Year { get; set; }
        // Max speed
        public double MaxSpeed { get; set; }
        // Vehicle color
        public string Color { get; set; }

        // Constructor
        public Vehicle()
        {
            Name = "Unknown";
            Year = DateTime.Now.Year;
            MaxSpeed = 0;
            Color = "Black";
        }

        // Returns type name
        public abstract string GetVehicleType();
        // Returns specific details
        public abstract string GetDetails();
    }
}