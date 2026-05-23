using System;
using System.Collections.Generic;
using Lab1.Shapes;

namespace Lab1.ShapeList
{
    // Collection of figures
    public class ShapeCollection
    {
        private List<Shape> shapes = new List<Shape>();
        private GdiShapeRenderer _renderer;

        // Constructor — get GdiShapeRenderer to have access to RegisterPluginRenderer
        public ShapeCollection(GdiShapeRenderer renderer)
        {
            _renderer = renderer;
        }

        public GdiShapeRenderer Renderer => _renderer;

        // Add figure
        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
        }

        // Delete figure
        public bool RemoveShape(Shape shape)
        {
            return shapes.Remove(shape);
        }

        // Clear all
        public void Clear()
        {
            shapes.Clear();
        }

        // Figures count
        public int Count => shapes.Count;

        // List of figures (read only)
        public IReadOnlyList<Shape> Shapes => shapes.AsReadOnly();

        // Draw all figures
        public void DrawAll(Graphics g)
        {
            foreach (Shape shape in shapes)
            {
                try
                {
                    // dynamic — for standard shapes it will call the appropriate overloaded Draw
                    // For plugin shapes — will throw RuntimeBinderException, we'll catch it and call Draw(Shape, g)
                    _renderer.Draw((dynamic)shape, g);
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                {
                    // Shape from a plugin — call the universal renderer
                    _renderer.Draw(shape, g);
                }
            }
        }
    }
}
