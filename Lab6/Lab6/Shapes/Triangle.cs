using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Shapes
{
    // Triangle class - inherits from Shape
    public class Triangle : Shape
    {
        // Array of three points
        public Point[] points;

        // Constructor
        public Triangle(Color color, Point location, Point point1, Point point2)
            : base(location, color)
        {
            points = new Point[3] { location, point1, point2 };
        }
    }
}