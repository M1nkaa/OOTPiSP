using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab1.Shapes
{
    // Абстрактный класс - нельзя создать экземпляр, только наследоваться
    public abstract class Shape
    {
        // Общие свойства для всех фигур
        public Point Location { get; set; } // позиция
        public Color Color { get; set; }    // цвет

        // Конструктор базового класса
        public Shape(Point location, Color color)
        {
            Location = location;
            Color = color;
        }

        // Виртуальный метод - может быть переопределен в наследниках
        public virtual void Draw(Graphics g)
        {
        }
    }
}