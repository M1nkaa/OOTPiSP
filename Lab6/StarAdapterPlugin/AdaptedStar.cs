using System;
using System.Drawing;
using Lab1.Shapes;

namespace StarAdapterPlugin
{
    // ============================================================
    //  ADAPTER PATTERN  --  object adapter (part 1 of 3).
    //
    //  Problem: the partner's StarPlugin.Star is a plain class that
    //  does NOT inherit from our Lab1.Shapes.Shape. Our application
    //  can only store and render objects of type Shape.
    //
    //  Solution: AdaptedStar IS-A Shape (so the host accepts it) and
    //  HAS-A StarPlugin.Star (so it can reuse the partner's geometry).
    //  This is the classic "object adapter": adapt by composition.
    // ============================================================

    /// <summary>
    /// Host-compatible shape that wraps a partner <see cref="StarPlugin.Star"/>.
    /// </summary>
    public class AdaptedStar : Shape
    {
        /// <summary>
        /// The wrapped partner object (the Adaptee instance).
        /// </summary>
        public StarPlugin.Star Partner { get; }

        // The three properties below expose the partner's data with the
        // exact names ("OuterRadius", "InnerRadius", "Points") that
        // Lab1.Shapes.ShapeSerializer looks for via reflection.
        // Thanks to them an AdaptedStar can be saved and loaded.
        public int OuterRadius => Partner.OuterRadius;
        public int InnerRadius => Partner.InnerRadius;
        public int Points => Partner.Points;

        /// <summary>
        /// Creation-time constructor: wrap a Star the partner factory
        /// has just produced. Used by <see cref="StarFactoryAdapter"/>.
        /// </summary>
        public AdaptedStar(StarPlugin.Star partner, Color color)
            : base(new Point(partner.CenterX, partner.CenterY), color)
        {
            Partner = partner;
        }

        /// <summary>
        /// Reconstruction constructor with signature (Point, Color, int, int, int).
        /// ShapeSerializer.Deserialize calls exactly this signature when it
        /// loads a star from a saved file, so the shape round-trips correctly.
        /// </summary>
        public AdaptedStar(Point location, Color color, int outerRadius, int innerRadius, int points)
            : base(location, color)
        {
            // Rebuild an equivalent partner object from the persisted values.
            Partner = new StarPlugin.Star(location.X, location.Y, outerRadius, innerRadius, points);
        }
    }
}
