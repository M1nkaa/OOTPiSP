using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Shapes
{
    // Interface for shape renderers
    public interface IShapeRenderer
    {
        void Draw(Circle circle, Graphics g);
        void Draw(Ellipse ellipse, Graphics g);
        void Draw(Line line, Graphics g);
        void Draw(MyRectangle rectangle, Graphics g);
        void Draw(Square square, Graphics g);
        void Draw(Triangle triangle, Graphics g);
    }
}