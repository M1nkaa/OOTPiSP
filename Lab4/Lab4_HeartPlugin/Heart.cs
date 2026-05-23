using System;
using System.Drawing;
using Lab1.Shapes;

namespace HeartPlugin
{
    public class Heart : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Heart(Point location, Color color, int width = 70, int height = 65)
            : base(location, color)
        {
            Width = width;
            Height = height;
        }

        public PointF[] GetHeartPoints()
        {
            PointF[] points = new PointF[20];
            float cx = Location.X;
            float cy = Location.Y;
            float scale = Math.Min(Width, Height) / 70f;

            for (int i = 0; i < 20; i++)
            {
                double t = i * Math.PI / 10.0;

                double x = 16 * Math.Pow(Math.Sin(t), 3);
                double y = 13 * Math.Cos(t)
                         - 5 * Math.Cos(2 * t)
                         - 2 * Math.Cos(3 * t)
                         - Math.Cos(4 * t);

                // Инвертируем Y, чтобы сердце смотрело вверх
                points[i] = new PointF(
                    cx + (float)(scale * x),
                    cy - (float)(scale * y)   
                );
            }

            return points;
        }
    }
}