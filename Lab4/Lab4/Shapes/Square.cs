using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Shapes
{
    // Square class - inherits from MyRectangle
    public class Square : MyRectangle
    {
        // Constructor
        public Square(Point location, Color color, int width)
            : base(location, color, width, width)
        {
        }
    }
}