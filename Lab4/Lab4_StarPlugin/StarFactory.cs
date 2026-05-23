using System;
using System.Drawing;
using Lab1.Shapes;

namespace StarPlugin
{
    // Фабрика для создания звёзд
    public class StarFactory : IShapeFactory
    {
        public string ShapeName => "Star (Plugin)";

        // Параметры: [0] = outerRadius (width), [1] = innerRadius (height), [2] = color
        public Shape CreateShape(Point startPoint, params object[] parameters)
        {
            int outerRadius = 50;
            int innerRadius = 25;
            Color color = Color.Red;

            if (parameters.Length >= 1) outerRadius = Convert.ToInt32(parameters[0]);
            if (parameters.Length >= 2) innerRadius = Convert.ToInt32(parameters[1]);
            if (parameters.Length >= 3 && parameters[parameters.Length - 1] is Color)
                color = (Color)parameters[parameters.Length - 1];

            return new Star(startPoint, color, outerRadius, innerRadius);
        }
    }
}
