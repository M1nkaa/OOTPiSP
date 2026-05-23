using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Lab1.ShapeList;
using Lab1.Shapes;

namespace Lab1.Patterns
{
    // ============================================================
    //  COMMAND PATTERN  --  concrete command "clear the canvas".
    //
    //  Execute(): snapshot every shape, then wipe the canvas.
    //  Undo()   : restore the canvas from the snapshot.
    //
    //  Storing the snapshot inside the command object is what makes
    //  even a destructive "Clear all" action fully reversible.
    // ============================================================

    /// <summary>
    /// Reversible command that removes all shapes from the editor.
    /// </summary>
    public class ClearCommand : ICommand
    {
        private readonly ShapeCollection _collection;
        private readonly ListBox _listBox;

        // Saved state captured at Execute() time.
        private List<Shape> _shapeBackup = new List<Shape>();
        private List<string> _listBackup = new List<string>();

        public string Description => "Clear canvas";

        public ClearCommand(ShapeCollection collection, ListBox listBox)
        {
            _collection = collection;
            _listBox = listBox;
        }

        // Remember the current content, then clear everything.
        public void Execute()
        {
            _shapeBackup = _collection.Shapes.ToList();
            _listBackup = _listBox.Items.Cast<object>()
                                        .Select(o => o?.ToString() ?? string.Empty)
                                        .ToList();

            _collection.Clear();
            _listBox.Items.Clear();
        }

        // Put every saved shape and list entry back.
        public void Undo()
        {
            _collection.Clear();
            foreach (var s in _shapeBackup)
                _collection.AddShape(s);

            _listBox.Items.Clear();
            foreach (var entry in _listBackup)
                _listBox.Items.Add(entry);
        }
    }
}
