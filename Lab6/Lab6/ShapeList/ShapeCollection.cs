using System;
using System.Collections.Generic;
using Lab1.Shapes;

namespace Lab1.ShapeList
{
    // Коллекция фигур
    public class ShapeCollection
    {
        private List<Shape> shapes = new List<Shape>();
        private GdiShapeRenderer _renderer;

        // Конструктор — принимаем GdiShapeRenderer чтобы иметь доступ к RegisterPluginRenderer
        public ShapeCollection(GdiShapeRenderer renderer)
        {
            _renderer = renderer;
        }

        public GdiShapeRenderer Renderer => _renderer;

        // Добавить фигуру
        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
        }

        // Удалить фигуру
        public bool RemoveShape(Shape shape)
        {
            return shapes.Remove(shape);
        }

        // Очистить всё
        public void Clear()
        {
            shapes.Clear();
        }

        // Количество фигур
        public int Count => shapes.Count;

        // Список фигур (только чтение)
        public IReadOnlyList<Shape> Shapes => shapes.AsReadOnly();

        // Нарисовать все фигуры
        public void DrawAll(Graphics g)
        {
            foreach (Shape shape in shapes)
            {
                try
                {
                    // dynamic — для стандартных фигур вызовет нужный перегруженный Draw
                    // Для плагинных — упадёт в RuntimeBinderException, поймаем и вызовем Draw(Shape, g)
                    _renderer.Draw((dynamic)shape, g);
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                {
                    // Фигура из плагина — вызываем универсальный рендерер
                    _renderer.Draw(shape, g);
                }
            }
        }
    }
}
