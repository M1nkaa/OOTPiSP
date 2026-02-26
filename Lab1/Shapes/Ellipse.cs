using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Shapes
{
    public class Ellipse : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Ellipse(Point location, Color color, int width, int height)
            : base(location, color)
        {
            Width = width;
            Height = height;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color))
            {
                g.DrawEllipse(pen, Location.X, Location.Y, Width, Height);
            }
        }
    }
}
