using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Shapes
{
    public class Line : Shape
    {
        public Point EndPoint { get; set; }

        public Line(Point startPoint, Point endPoint, Color color)
            : base(startPoint, color)
        {
            EndPoint = endPoint;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color))
            {
                g.DrawLine(pen, Location, EndPoint);
            }
        }
    }
}
