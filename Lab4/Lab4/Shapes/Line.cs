using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Shapes
{
    // Line class - inherits from Shape
    public class Line : Shape
    {
        // End point of the line
        public Point EndPoint { get; set; }

        // Constructor
        public Line(Point startPoint, Point endPoint, Color color)
            : base(startPoint, color)
        {
            EndPoint = endPoint;
        }
    }
}