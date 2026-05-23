using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Shapes
{
    // Interface for shape factories
    // Each factory creates a specific type of shape
    public interface IShapeFactory
    {
        // Name of the shape to display in UI
        string ShapeName { get; }

        // Creates a shape with given parameters
        // startPoint: location where user clicked
        // parameters: size, color, etc.
        Shape CreateShape(Point startPoint, params object[] parameters);
    }

    // Factory for Rectangle
    public class RectangleFactory : IShapeFactory
    {
        public string ShapeName => "Rectangle";

        // Creates a rectangle using width, height and color
        public Shape CreateShape(Point startPoint, params object[] parameters)
        {
            // Check if we have enough parameters
            if (parameters.Length < 3)
                throw new ArgumentException("Rectangle requires width, height, and color parameters");

            int width = Convert.ToInt32(parameters[0]);
            int height = Convert.ToInt32(parameters[1]);
            Color color = (Color)parameters[2];

            return new MyRectangle(startPoint, color, width, height);
        }
    }

    // Factory for Ellipse
    public class EllipseFactory : IShapeFactory
    {
        public string ShapeName => "Ellipse";

        // Creates an ellipse using width, height and color
        public Shape CreateShape(Point startPoint, params object[] parameters)
        {
            if (parameters.Length < 3)
                throw new ArgumentException("Ellipse requires width, height, and color parameters");

            int width = Convert.ToInt32(parameters[0]);
            int height = Convert.ToInt32(parameters[1]);
            Color color = (Color)parameters[2];

            return new Ellipse(startPoint, color, width, height);
        }
    }

    // Factory for Line
    public class LineFactory : IShapeFactory
    {
        public string ShapeName => "Line";

        // Creates a line using end point and color
        public Shape CreateShape(Point startPoint, params object[] parameters)
        {
            if (parameters.Length < 2)
                throw new ArgumentException("Line requires end point and color parameters");

            Point endPoint = (Point)parameters[0];
            Color color = (Color)parameters[1];

            return new Line(startPoint, endPoint, color);
        }
    }

    // Factory for Square
    public class SquareFactory : IShapeFactory
    {
        public string ShapeName => "Square";

        // Creates a square using side length and color
        public Shape CreateShape(Point startPoint, params object[] parameters)
        {
            if (parameters.Length < 2)
                throw new ArgumentException("Square requires side length and color parameters");

            int sideLength = Convert.ToInt32(parameters[0]);
            Color color = (Color)parameters[1];

            return new Square(startPoint, color, sideLength);
        }
    }

    // Factory for Circle
    public class CircleFactory : IShapeFactory
    {
        public string ShapeName => "Circle";

        // Creates a circle using diameter and color
        public Shape CreateShape(Point startPoint, params object[] parameters)
        {
            if (parameters.Length < 2)
                throw new ArgumentException("Circle requires diameter and color parameters");

            int diameter = Convert.ToInt32(parameters[0]);
            Color color = (Color)parameters[1];

            return new Circle(startPoint, color, diameter);
        }
    }

    // Factory for Triangle
    public class TriangleFactory : IShapeFactory
    {
        public string ShapeName => "Triangle";

        // Creates a triangle using three points and color
        public Shape CreateShape(Point startPoint, params object[] parameters)
        {
            if (parameters.Length < 3)
                throw new ArgumentException("Triangle requires point1, point2, and color parameters");

            Point point1 = (Point)parameters[0];
            Point point2 = (Point)parameters[1];
            Color color = (Color)parameters[2];

            return new Triangle(color, startPoint, point1, point2);
        }
    }
}