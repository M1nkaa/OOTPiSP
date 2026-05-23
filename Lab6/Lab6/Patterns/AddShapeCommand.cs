using System;
using System.Linq;
using System.Windows.Forms;
using Lab1.ShapeList;
using Lab1.Shapes;

namespace Lab1.Patterns
{
    // ============================================================
    //  COMMAND PATTERN  --  concrete command "add a shape".
    //
    //  Execute(): add the shape to the canvas and to the list box.
    //  Undo()   : remove that same shape again.
    //
    //  Undo is implemented by rebuilding the collection without the
    //  shape, so it relies only on the public members that
    //  ShapeCollection is already known to expose (Shapes / AddShape
    //  / Clear). Because the form undoes commands in LIFO order, the
    //  undone shape's list-box entry is always the last one.
    // ============================================================

    /// <summary>
    /// Reversible command that adds a single shape to the editor.
    /// </summary>
    public class AddShapeCommand : ICommand
    {
        private readonly ShapeCollection _collection;
        private readonly ListBox _listBox;
        private readonly Shape _shape;
        private readonly string _listEntry;

        public string Description => $"Add {_shape.GetType().Name}";

        public AddShapeCommand(ShapeCollection collection, ListBox listBox,
                               Shape shape, string listEntry)
        {
            _collection = collection;
            _listBox = listBox;
            _shape = shape;
            _listEntry = listEntry;
        }

        // Add the shape to the model and mirror it in the list box.
        public void Execute()
        {
            _collection.AddShape(_shape);
            _listBox.Items.Add(_listEntry);
        }

        // Remove the shape again: rebuild the collection without it.
        public void Undo()
        {
            // Keep every shape except the one this command added.
            var remaining = _collection.Shapes
                                       .Where(s => !ReferenceEquals(s, _shape))
                                       .ToList();

            _collection.Clear();
            foreach (var s in remaining)
                _collection.AddShape(s);

            // The matching list-box entry is the most recently added one.
            if (_listBox.Items.Count > 0)
                _listBox.Items.RemoveAt(_listBox.Items.Count - 1);
        }
    }
}
