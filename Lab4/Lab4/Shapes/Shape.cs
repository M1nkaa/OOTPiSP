using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab1.Shapes
{
    // Abstract base class for all shapes
    public abstract class Shape
    {
        // Common properties
        public Point Location { get; set; }
        public Color Color { get; set; }

        // Base constructor
        public Shape(Point location, Color color)
        {
            Location = location;
            Color = color;
        }
    }
}