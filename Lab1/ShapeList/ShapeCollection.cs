using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.Shapes;

namespace Lab1.ShapeList
{
    public class ShapeCollection
    {
        private List<Shape> shapes = new List<Shape>();

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
        }

        public void DrawAll(Graphics g)
        {
            foreach (Shape shape in shapes)
            {
                shape.Draw(g); 
            }
        }
    }
}
