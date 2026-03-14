using System;
using System.Collections.Generic;

namespace Lab3.Models
{
    [Serializable]
    public class VehicleCollection
    {
        // List of vehicles
        public List<Vehicle> Vehicles { get; set; }

        // Constructor
        public VehicleCollection()
        {
            Vehicles = new List<Vehicle>();
        }

        // Add vehicle
        public void Add(Vehicle vehicle) => Vehicles.Add(vehicle);
        // Remove vehicle
        public void Remove(Vehicle vehicle) => Vehicles.Remove(vehicle);
        // Remove at index
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Vehicles.Count)
                Vehicles.RemoveAt(index);
        }
        // Clear all
        public void Clear() => Vehicles.Clear();
        // Get count
        public int Count => Vehicles.Count;
    }
}