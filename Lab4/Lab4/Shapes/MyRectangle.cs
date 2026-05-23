using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Shapes
{
    // Rectangle class - inherits from Shape
    public class MyRectangle : Shape
    {
        // Properties
        public int Width { get; set; }
        public int Height { get; set; }

        // Constructor
        public MyRectangle(Point location, Color color, int width, int height)
            : base(location, color)
        {
            Width = width;
            Height = height;
        }
    }
}