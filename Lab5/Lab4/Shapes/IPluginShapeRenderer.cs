using System.Drawing;

namespace Lab1.Shapes
{
    // Additional interface for plugins — plugin knows how to draw its own shape
    public interface IPluginShapeRenderer
    {
        void Draw(Shape shape, Graphics g);
    }
}