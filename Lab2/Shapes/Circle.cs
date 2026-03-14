using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Shapes
{
    // Circle class - inherits from Ellipse
    public class Circle : Ellipse
    {
        // Constructor
        public Circle(Point location, Color color, int width)
            : base(location, color, width, width)
        {
        }
    }
}