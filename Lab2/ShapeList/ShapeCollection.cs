using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.Shapes;

namespace Lab1.ShapeList
{
    // Collection of shapes
    public class ShapeCollection
    {
        private List<Shape> shapes = new List<Shape>();
        private IShapeRenderer _renderer;

        // Constructor
        public ShapeCollection(IShapeRenderer renderer)
        {
            _renderer = renderer;
        }

        // Add shape
        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
        }

        // Remove shape
        public bool RemoveShape(Shape shape)
        {
            return shapes.Remove(shape);
        }

        // Clear all shapes
        public void Clear()
        {
            shapes.Clear();
        }

        // Get number of shapes
        public int Count
        {
            get { return shapes.Count; }
        }

        // Get shapes (read-only)
        public IReadOnlyList<Shape> Shapes
        {
            get { return shapes.AsReadOnly(); }
        }

        // Draw all shapes
        public void DrawAll(Graphics g)
        {
            foreach (Shape shape in shapes)
            {
                _renderer.Draw((dynamic)shape, g);
            }
        }
    }
}