using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Shapes
{
    public class Square : MyRectangle
    {
        public Square(Point location, Color color, int width) 
            : base(location, color, width, width)
        {
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color))
            {
                g.DrawRectangle(pen, Location.X, Location.Y, Width, Height);
            }
        }
    }
}
