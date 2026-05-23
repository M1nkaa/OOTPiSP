using System;
using System.Drawing;
using Lab1.Shapes;

namespace StarAdapterPlugin
{
    // ============================================================
    //  ADAPTER PATTERN  --  rendering side of the adapter (part 3 of 3).
    //
    //  The host draws plugin shapes through IPluginShapeRenderer.
    //  This renderer adapts the partner's drawing data: it asks the
    //  partner Star (via AdaptedStar.Partner) for its polygon points
    //  and feeds them to the host's Graphics object.
    // ============================================================

    /// <summary>
    /// Renderer for <see cref="AdaptedStar"/> shapes.
    /// </summary>
    public class AdaptedStarRenderer : IPluginShapeRenderer
    {
        /// <summary>
        /// Tells the host which shape type this renderer handles.
        /// The host discovers this method by reflection while loading
        /// the plugin, exactly as it does for the built-in plugins.
        /// </summary>
        public Type GetShapeType() => typeof(AdaptedStar);

        /// <summary>
        /// Draw the adapted star by reusing the partner's geometry.
        /// </summary>
        public void Draw(Shape shape, Graphics g)
        {
            // Ignore anything that is not one of our adapted stars.
            if (shape is not AdaptedStar adapted) return;

            // Geometry is computed entirely by the partner's own code.
            Point[] points = adapted.Partner.GetPoints();

            using Pen pen = new Pen(adapted.Color, 2);
            g.DrawPolygon(pen, points);
        }
    }
}
