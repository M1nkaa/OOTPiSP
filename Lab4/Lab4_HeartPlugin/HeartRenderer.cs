using System;
using System.Drawing;
using Lab1.Shapes;

namespace HeartPlugin
{
    public class HeartRenderer : IPluginShapeRenderer
    {
        public Type GetShapeType() => typeof(Heart);

        public void Draw(Shape shape, Graphics g)
        {
            if (shape is not Heart heart) return;

            PointF[] pts = heart.GetHeartPoints();

            using (Pen pen = new Pen(heart.Color, 3))
            using (SolidBrush brush = new SolidBrush(heart.Color))
            {
                g.FillPolygon(brush, pts);     // Заливка
                g.DrawPolygon(pen, pts);       // Контур
            }
        }
    }
}