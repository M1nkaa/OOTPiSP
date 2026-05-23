using System;
using System.Drawing;
using Lab1.Shapes;

namespace HeartPlugin
{
    public class HeartFactory : IShapeFactory
    {
        public string ShapeName => "Heart (Plugin)";

        public Shape CreateShape(Point startPoint, params object[] parameters)
        {
            int width = 60;
            int height = 55;
            Color color = Color.Red;

            if (parameters.Length >= 1) width = Convert.ToInt32(parameters[0]);
            if (parameters.Length >= 2) height = Convert.ToInt32(parameters[1]);
            if (parameters.Length >= 3 && parameters[parameters.Length - 1] is Color c)
                color = c;

            return new Heart(startPoint, color, width, height);
        }
    }
}