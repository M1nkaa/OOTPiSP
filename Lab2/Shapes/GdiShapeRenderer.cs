using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Shapes
{
    // Renderer for drawing shapes using GDI+
    public class GdiShapeRenderer : IShapeRenderer
    {
        // Draw rectangle
        public void Draw(MyRectangle rectangle, Graphics g)
        {
            using (Pen pen = new Pen(rectangle.Color))
            {
                g.DrawRectangle(pen, rectangle.Location.X, rectangle.Location.Y, rectangle.Width, rectangle.Height);
            }
        }

        // Draw ellipse
        public void Draw(Ellipse ellipse, Graphics g)
        {
            using (Pen pen = new Pen(ellipse.Color))
            {
                g.DrawEllipse(pen, ellipse.Location.X, ellipse.Location.Y, ellipse.Width, ellipse.Height);
            }
        }

        // Draw line
        public void Draw(Line line, Graphics g)
        {
            using (Pen pen = new Pen(line.Color))
            {
                g.DrawLine(pen, line.Location, line.EndPoint);
            }
        }

        // Draw square
        public void Draw(Square square, Graphics g)
        {
            using (Pen pen = new Pen(square.Color))
            {
                g.DrawRectangle(pen, square.Location.X, square.Location.Y, square.Width, square.Height);
            }
        }

        // Draw circle
        public void Draw(Circle circle, Graphics g)
        {
            using (Pen pen = new Pen(circle.Color))
            {
                g.DrawEllipse(pen, circle.Location.X, circle.Location.Y, circle.Width, circle.Height);
            }
        }

        // Draw triangle
        public void Draw(Triangle triangle, Graphics g)
        {
            if (triangle.points == null || triangle.points.Length < 3)
                return;

            using (Pen pen = new Pen(triangle.Color))
            {
                g.DrawLine(pen, triangle.points[0], triangle.points[1]);
                g.DrawLine(pen, triangle.points[1], triangle.points[2]);
                g.DrawLine(pen, triangle.points[2], triangle.points[0]);
            }
        }
    }
}