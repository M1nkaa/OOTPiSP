using Lab1.ShapeList;
using Lab1.Shapes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Figures : Form
    {
        private ShapeCollection shapeCollection;

        public Figures()
        {
            InitializeComponent();
            this.Paint += Figures_Paint;
            InitializeShapes();
        }

        private void InitializeShapes()
        {
            shapeCollection = new ShapeCollection();

            shapeCollection.AddShape(new MyRectangle(new Point(50, 50), Color.Blue, 100, 60));
            shapeCollection.AddShape(new Ellipse(new Point(200, 50), Color.Red, 80, 100));
            shapeCollection.AddShape(new Line(new Point(50, 320), new Point(200, 370), Color.Green));
            shapeCollection.AddShape(new Square(new Point(350, 50), Color.Orange, 70));
            shapeCollection.AddShape(new Circle(new Point(350, 200), Color.Purple, 50));
            shapeCollection.AddShape(new Triangle(Color.Black, new Point(100, 250), new Point(200, 250), new Point(150, 150)));
        }

        private void Figures_Paint(object sender, PaintEventArgs e)
        {
            if (shapeCollection != null)
            {
                shapeCollection.DrawAll(e.Graphics);
            }
        }
    }
}