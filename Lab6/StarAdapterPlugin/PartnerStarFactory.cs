using System;
using PluginInterface;
using Microsoft.VisualBasic;

namespace StarPlugin
{
    // ============================================================
    //  PARTNER'S CODE  --  received from another student.
    //  This is the core "Adaptee": a factory that implements the
    //  partner's IShapeFactoryPlugin contract. Its protocol is
    //  click-based (AddClick -> IsReady -> CreateShape) and it asks
    //  the user for parameters through Visual Basic InputBox dialogs.
    //  Logic is untouched; only English comments were added.
    // ============================================================

    /// <summary>
    /// Partner factory that produces <see cref="Star"/> objects.
    /// </summary>
    public class StarFactory : IShapeFactoryPlugin
    {
        // Accumulated click position and a flag telling whether we have it.
        private int _clickX, _clickY;
        private bool _hasClick = false;

        public string ShapeName => "Star";
        public int NeededClicks => 1;

        // Store the first click; further clicks are ignored until Reset().
        public void AddClick(int x, int y)
        {
            if (!_hasClick)
            {
                _clickX = x;
                _clickY = y;
                _hasClick = true;
            }
        }

        // The factory is ready as soon as one click has been received.
        public bool IsReady() => _hasClick;

        // Ask the user for star parameters and build the Star object.
        public object CreateShape()
        {
            string outerStr = Interaction.InputBox("Enter outer radius:", "Create Star", "50");
            string innerStr = Interaction.InputBox("Enter inner radius:", "Create Star", "25");
            string pointsStr = Interaction.InputBox("Enter number of points (5-12):", "Create Star", "5");

            int outerRadius = 50, innerRadius = 25, points = 5;
            int.TryParse(outerStr, out outerRadius);
            int.TryParse(innerStr, out innerRadius);
            int.TryParse(pointsStr, out points);
            points = Math.Clamp(points, 5, 12);

            return new Star(_clickX, _clickY, outerRadius, innerRadius, points);
        }

        // Forget the accumulated click so the factory can be reused.
        public void Reset() => _hasClick = false;

        // Return the collected click (or an empty array if none yet).
        public (int X, int Y)[] GetCoordinates() =>
            _hasClick ? new[] { (_clickX, _clickY) } : Array.Empty<(int, int)>();
    }
}
