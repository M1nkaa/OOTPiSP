using System;
using System.Drawing;
using Lab1.Shapes;

namespace StarPlugin
{
    // Рендерер для звезды — реализует IPluginShapeRenderer
    public class StarRenderer : IPluginShapeRenderer
    {
        // Сообщает основной программе, для какого типа фигуры этот рендерер
        public Type GetShapeType() => typeof(Star);

        public void Draw(Shape shape, Graphics g)
        {
            if (shape is not Star star) return;

            PointF[] pts = star.GetStarPoints();

            using (Pen pen = new Pen(star.Color, 2))
            {
                // Рисуем звезду — соединяем все точки
                g.DrawPolygon(pen, pts);
            }
        }
    }
}
