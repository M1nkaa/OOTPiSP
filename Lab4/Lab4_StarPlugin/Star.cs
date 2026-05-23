using System;
using System.Drawing;
using Lab1.Shapes;

namespace StarPlugin
{
    // Новая фигура — Звезда (5-конечная)
    // Наследуется от Shape из основного проекта
    public class Star : Shape
    {
        public int OuterRadius { get; set; }  // Внешний радиус
        public int InnerRadius { get; set; }  // Внутренний радиус (углубления)
        public int Points { get; set; }       // Количество лучей

        public Star(Point location, Color color, int outerRadius, int innerRadius = 0, int points = 5)
            : base(location, color)
        {
            OuterRadius = outerRadius;
            InnerRadius = innerRadius > 0 ? innerRadius : outerRadius / 2;
            Points = points > 2 ? points : 5;
        }

        // Вычисляем вершины звезды
        public PointF[] GetStarPoints()
        {
            int totalPoints = Points * 2; // Чередуем внешние и внутренние
            PointF[] pts = new PointF[totalPoints];
            double angleStep = Math.PI / Points; // Угол между соседними вершинами
            double startAngle = -Math.PI / 2;    // Начинаем сверху

            for (int i = 0; i < totalPoints; i++)
            {
                double angle = startAngle + i * angleStep;
                int radius = (i % 2 == 0) ? OuterRadius : InnerRadius;
                pts[i] = new PointF(
                    Location.X + (float)(radius * Math.Cos(angle)),
                    Location.Y + (float)(radius * Math.Sin(angle))
                );
            }

            return pts;
        }
    }
}
