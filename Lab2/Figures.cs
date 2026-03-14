using Lab1.ShapeList;
using Lab1.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Figures : Form
    {
        private ShapeCollection shapeCollection;
        private List<IShapeFactory> factories = new List<IShapeFactory>();
        private Color currentColor = Color.Black;
        private Point? firstClick = null; // For lines

        public Figures()
        {
            InitializeComponent();

            // Subscribe to events
            this.Paint += Figures_Paint;
            this.MouseClick += Figures_MouseClick;

            // Create factories for all shapes
            factories.Add(new RectangleFactory());
            factories.Add(new EllipseFactory());
            factories.Add(new LineFactory());
            factories.Add(new SquareFactory());
            factories.Add(new CircleFactory());
            factories.Add(new TriangleFactory());

            // Add shape names to ComboBox
            foreach (var factory in factories)
            {
                comboBoxShapeType.Items.Add(factory.ShapeName);
            }
            comboBoxShapeType.SelectedIndex = 0;

            // Create collection with renderer
            shapeCollection = new ShapeCollection(new GdiShapeRenderer());

            // Set initial button color
            buttonColor.BackColor = currentColor;
            buttonColor.Text = "";
        }

        // Draw all shapes
        private void Figures_Paint(object sender, PaintEventArgs e)
        {
            if (shapeCollection != null)
            {
                e.Graphics.Clear(Color.White);
                shapeCollection.DrawAll(e.Graphics);
            }
        }

        // Handle mouse clicks
        private void Figures_MouseClick(object sender, MouseEventArgs e)
        {
            if (comboBoxShapeType.SelectedIndex < 0) return;

            IShapeFactory factory = factories[comboBoxShapeType.SelectedIndex];
            Shape newShape = null;

            // Line needs two clicks
            if (factory is LineFactory)
            {
                if (firstClick == null)
                {
                    firstClick = e.Location;
                    Text = "Now select end point";
                    return;
                }
                else
                {
                    newShape = factory.CreateShape(firstClick.Value, e.Location, currentColor);
                    firstClick = null;
                    Text = "Graphics Editor";
                }
            }
            // Triangle
            else if (factory is TriangleFactory)
            {
                int width = (int)numericWidth.Value;
                int height = (int)numericHeight.Value;

                Point p1 = e.Location;
                Point p2 = new Point(e.X + width / 2, e.Y + height);
                Point p3 = new Point(e.X - width / 2, e.Y + height);

                newShape = new Triangle(currentColor, p1, p2, p3);
            }
            // Other shapes - create with one click
            else
            {
                int width = (int)numericWidth.Value;
                int height = (int)numericHeight.Value;

                if (factory is CircleFactory)
                {
                    newShape = factory.CreateShape(e.Location, width, currentColor);
                }
                else if (factory is SquareFactory)
                {
                    newShape = factory.CreateShape(e.Location, width, currentColor);
                }
                else
                {
                    newShape = factory.CreateShape(e.Location, width, height, currentColor);
                }
            }

            if (newShape != null)
            {
                shapeCollection.AddShape(newShape);
                listBox.Items.Add($"{newShape.GetType().Name} - {currentColor.Name}");
                Invalidate();
            }
        }

        // Color selection
        private void buttonColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = currentColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
                buttonColor.BackColor = currentColor;
            }
        }

        // Clear all shapes
        private void buttonClear_Click(object sender, EventArgs e)
        {
            shapeCollection.Clear();
            listBox.Items.Clear();
            Invalidate();
        }
    }
}