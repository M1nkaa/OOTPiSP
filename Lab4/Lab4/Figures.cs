using Lab1.ShapeList;
using Lab1.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Figures : Form
    {
        private ShapeCollection shapeCollection;
        private GdiShapeRenderer renderer;
        private List<IShapeFactory> factories = new List<IShapeFactory>();
        private Color currentColor = Color.Black;
        private Point? firstClick = null;
        private Point? secondClick = null; // Для треугольника

        public Figures()
        {
            InitializeComponent();

            this.Paint += Figures_Paint;
            this.MouseClick += Figures_MouseClick;

            renderer = new GdiShapeRenderer();
            shapeCollection = new ShapeCollection(renderer);

            // Стандартные фабрики
            factories.Add(new RectangleFactory());
            factories.Add(new EllipseFactory());
            factories.Add(new LineFactory());
            factories.Add(new SquareFactory());
            factories.Add(new CircleFactory());
            factories.Add(new TriangleFactory());

            // Загружаем плагины из папки plugins/
            LoadPlugins();

            // Заполняем ComboBox
            foreach (var factory in factories)
            {
                comboBoxShapeType.Items.Add(factory.ShapeName);
            }
            comboBoxShapeType.SelectedIndex = 0;

            buttonColor.BackColor = currentColor;
            buttonColor.Text = "";
        }

        // Динамическая загрузка плагинов из папки plugins/
        private void LoadPlugins()
        {
            string pluginsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");
            if (!Directory.Exists(pluginsDir))
            {
                Directory.CreateDirectory(pluginsDir);
                return;
            }

            foreach (string dllPath in Directory.GetFiles(pluginsDir, "*.dll"))
            {
                string error;
                if (!PluginSignatureVerifier.VerifyPlugin(dllPath, out error))
                {
                    MessageBox.Show($"Плагин {Path.GetFileName(dllPath)} отклонён:\n{error}",
                        "Проверка подписи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }
                try
                {
                    Assembly assembly = Assembly.LoadFrom(dllPath);

                    foreach (Type type in assembly.GetTypes())
                    {
                        // Ищем классы IShapeFactory
                        if (typeof(IShapeFactory).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                        {
                            IShapeFactory factory = (IShapeFactory)Activator.CreateInstance(type);
                            factories.Add(factory);
                        }

                        // Ищем классы IPluginShapeRenderer
                        if (typeof(IPluginShapeRenderer).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                        {
                            IPluginShapeRenderer pluginRenderer = (IPluginShapeRenderer)Activator.CreateInstance(type);

                            // Узнаём, для какого типа фигуры этот рендерер
                            // Ищем атрибут или метод GetShapeType()
                            MethodInfo getShapeType = type.GetMethod("GetShapeType");
                            if (getShapeType != null)
                            {
                                Type shapeType = (Type)getShapeType.Invoke(pluginRenderer, null);
                                renderer.RegisterPluginRenderer(shapeType, pluginRenderer);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки плагина {Path.GetFileName(dllPath)}:\n{ex.Message}",
                        "Ошибка плагина", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Figures_Paint(object sender, PaintEventArgs e)
        {
            if (shapeCollection != null)
            {
                e.Graphics.Clear(Color.White);
                shapeCollection.DrawAll(e.Graphics);
            }
        }

        private void Figures_MouseClick(object sender, MouseEventArgs e)
        {
            if (comboBoxShapeType.SelectedIndex < 0) return;

            IShapeFactory factory = factories[comboBoxShapeType.SelectedIndex];
            Shape newShape = null;

            if (factory is LineFactory)
            {
                if (firstClick == null)
                {
                    firstClick = e.Location;
                    Text = "Кликните для конечной точки линии";
                    return;
                }
                else
                {
                    newShape = factory.CreateShape(firstClick.Value, e.Location, currentColor);
                    firstClick = null;
                    Text = "Graphics Editor";
                }
            }
            else if (factory is TriangleFactory)
            {
                int width = (int)numericWidth.Value;
                int height = (int)numericHeight.Value;

                Point p1 = e.Location;
                Point p2 = new Point(e.X + width / 2, e.Y + height);
                Point p3 = new Point(e.X - width / 2, e.Y + height);

                newShape = new Triangle(currentColor, p1, p2, p3);
            }
            else
            {
                int width = (int)numericWidth.Value;
                int height = (int)numericHeight.Value;

                if (factory is CircleFactory || factory is SquareFactory)
                {
                    newShape = factory.CreateShape(e.Location, width, currentColor);
                }
                else if (factory is RectangleFactory || factory is EllipseFactory)
                {
                    newShape = factory.CreateShape(e.Location, width, height, currentColor);
                }
                else
                {
                    // Плагинная фигура — пробуем с width и height
                    try
                    {
                        newShape = factory.CreateShape(e.Location, width, height, currentColor);
                    }
                    catch
                    {
                        try { newShape = factory.CreateShape(e.Location, width, currentColor); }
                        catch { newShape = factory.CreateShape(e.Location, currentColor); }
                    }
                }
            }

            if (newShape != null)
            {
                shapeCollection.AddShape(newShape);
                listBox.Items.Add($"{newShape.GetType().Name} - {currentColor.Name}");
                Invalidate();
            }
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = currentColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
                buttonColor.BackColor = currentColor;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            shapeCollection.Clear();
            listBox.Items.Clear();
            Invalidate();
        }
    }
}
