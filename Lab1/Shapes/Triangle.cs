using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Shapes
{
    public class Triangle : Shape
    {
        public Point[] points;

        public Triangle(Color color, Point location, Point point1, Point point2)
            : base(location, color)
        {
            points = new Point[3] { location, point1, point2 };
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color))
            {
                g.DrawLine(pen, points[0], points[1]);
                g.DrawLine(pen, points[1], points[2]);
                g.DrawLine(pen, points[2], points[0]);
            }
        }
    }
}
