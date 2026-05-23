using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;

namespace Lab1.Shapes
{
    /// <summary>
    /// Handles serialization and deserialization of shapes to/from JSON format.
    /// Supports all built-in shapes and plugin shapes via reflection.
    /// </summary>
    public static class ShapeSerializer
    {
        // DTO used for JSON serialization of a single shape
        private class ShapeDto
        {
            public string TypeName { get; set; } = "";
            public int X { get; set; }
            public int Y { get; set; }
            public int R { get; set; }   // Red channel
            public int G { get; set; }   // Green channel
            public int B { get; set; }   // Blue channel
            public int Width { get; set; }
            public int Height { get; set; }
            public int X2 { get; set; }  // Second point X (Line end / Triangle P2)
            public int Y2 { get; set; }  // Second point Y
            public int X3 { get; set; }  // Third point X (Triangle P3)
            public int Y3 { get; set; }  // Third point Y
            public int OuterRadius { get; set; }  // Star outer radius
            public int InnerRadius { get; set; }  // Star inner radius
            public int Points { get; set; }       // Star points count
        }

        /// <summary>
        /// Serialize a list of shapes to a JSON string.
        /// </summary>
        public static string Serialize(IEnumerable<Shape> shapes)
        {
            var dtos = new List<ShapeDto>();

            foreach (var shape in shapes)
            {
                var dto = new ShapeDto
                {
                    TypeName = shape.GetType().Name,
                    X = shape.Location.X,
                    Y = shape.Location.Y,
                    R = shape.Color.R,
                    G = shape.Color.G,
                    B = shape.Color.B
                };

                // Fill type-specific fields
                switch (shape)
                {
                    case Square sq:
                        // Square before MyRectangle (it inherits from it)
                        dto.Width = sq.Width;
                        break;
                    case Circle circ:
                        // Circle before Ellipse
                        dto.Width = circ.Width;
                        break;
                    case MyRectangle rect:
                        dto.Width = rect.Width;
                        dto.Height = rect.Height;
                        break;
                    case Ellipse ell:
                        dto.Width = ell.Width;
                        dto.Height = ell.Height;
                        break;
                    case Line ln:
                        dto.X2 = ln.EndPoint.X;
                        dto.Y2 = ln.EndPoint.Y;
                        break;
                    case Triangle tri:
                        dto.X2 = tri.points[1].X;
                        dto.Y2 = tri.points[1].Y;
                        dto.X3 = tri.points[2].X;
                        dto.Y3 = tri.points[2].Y;
                        break;
                    default:
                        // Plugin shapes: extract known properties via reflection
                        SerializePluginShape(shape, dto);
                        break;
                }

                dtos.Add(dto);
            }

            return JsonSerializer.Serialize(dtos, new JsonSerializerOptions { WriteIndented = true });
        }

        /// <summary>
        /// Capture plugin shape properties via reflection.
        /// </summary>
        private static void SerializePluginShape(Shape shape, ShapeDto dto)
        {
            var type = shape.GetType();

            var props = new[] { "Width", "Height", "OuterRadius", "InnerRadius", "Points" };
            foreach (var propName in props)
            {
                var val = type.GetProperty(propName)?.GetValue(shape);
                if (val is int i)
                {
                    switch (propName)
                    {
                        case "Width":       dto.Width       = i; break;
                        case "Height":      dto.Height      = i; break;
                        case "OuterRadius": dto.OuterRadius = i; break;
                        case "InnerRadius": dto.InnerRadius = i; break;
                        case "Points":      dto.Points      = i; break;
                    }
                }
            }
        }

        /// <summary>
        /// Deserialize a JSON string into a list of shapes.
        /// Plugin shapes are reconstructed using loaded assemblies if available.
        /// </summary>
        public static List<Shape> Deserialize(
            string json,
            IEnumerable<System.Reflection.Assembly>? pluginAssemblies = null)
        {
            var result = new List<Shape>();

            var dtos = JsonSerializer.Deserialize<List<ShapeDto>>(json);
            if (dtos == null) return result;

            foreach (var dto in dtos)
            {
                var color = Color.FromArgb(dto.R, dto.G, dto.B);
                var location = new Point(dto.X, dto.Y);
                Shape? shape = null;

                switch (dto.TypeName)
                {
                    case "MyRectangle":
                        shape = new MyRectangle(location, color, dto.Width, dto.Height);
                        break;
                    case "Ellipse":
                        shape = new Ellipse(location, color, dto.Width, dto.Height);
                        break;
                    case "Square":
                        shape = new Square(location, color, dto.Width);
                        break;
                    case "Circle":
                        shape = new Circle(location, color, dto.Width);
                        break;
                    case "Line":
                        shape = new Line(location, new Point(dto.X2, dto.Y2), color);
                        break;
                    case "Triangle":
                        shape = new Triangle(color, location,
                            new Point(dto.X2, dto.Y2),
                            new Point(dto.X3, dto.Y3));
                        break;
                    default:
                        // Try to find and create plugin shape via reflection
                        shape = TryDeserializePluginShape(dto, location, color, pluginAssemblies);
                        break;
                }

                if (shape != null)
                    result.Add(shape);
            }

            return result;
        }

        /// <summary>
        /// Try to reconstruct a plugin shape via reflection.
        /// Tries multiple constructor signatures in order.
        /// </summary>
        private static Shape? TryDeserializePluginShape(
            ShapeDto dto, Point location, Color color,
            IEnumerable<System.Reflection.Assembly>? assemblies)
        {
            if (assemblies == null) return null;

            foreach (var asm in assemblies)
            {
                foreach (var type in asm.GetTypes())
                {
                    if (type.Name != dto.TypeName) continue;
                    if (!typeof(Shape).IsAssignableFrom(type)) continue;

                    // Star: (Point, Color, int outer, int inner, int points)
                    try
                    {
                        var ctor = type.GetConstructor(new[] {
                            typeof(Point), typeof(Color), typeof(int), typeof(int), typeof(int) });
                        if (ctor != null)
                            return (Shape)ctor.Invoke(new object[] {
                                location, color, dto.OuterRadius, dto.InnerRadius, dto.Points });
                    }
                    catch { }

                    // Heart: (Point, Color, int width, int height)
                    try
                    {
                        var ctor = type.GetConstructor(new[] {
                            typeof(Point), typeof(Color), typeof(int), typeof(int) });
                        if (ctor != null)
                            return (Shape)ctor.Invoke(new object[] {
                                location, color, dto.Width, dto.Height });
                    }
                    catch { }

                    // Fallback: (Point, Color, int)
                    try
                    {
                        var ctor = type.GetConstructor(new[] { typeof(Point), typeof(Color), typeof(int) });
                        if (ctor != null)
                            return (Shape)ctor.Invoke(new object[] { location, color, dto.Width });
                    }
                    catch { }

                    // Minimal: (Point, Color)
                    try
                    {
                        var ctor = type.GetConstructor(new[] { typeof(Point), typeof(Color) });
                        if (ctor != null)
                            return (Shape)ctor.Invoke(new object[] { location, color });
                    }
                    catch { }
                }
            }

            return null;
        }
    }
}
