using System;
using System.Drawing;

namespace StarPlugin
{
    // ============================================================
    //  PARTNER'S CODE  --  received from another student.
    //  This is part of the "Adaptee": a plain Star class that does
    //  NOT inherit from our Lab1.Shapes.Shape base class and knows
    //  nothing about our application. Logic is left untouched;
    //  only English comments were added to satisfy the lab rules.
    // ============================================================

    /// <summary>
    /// A star shape described by a center, two radii and a ray count.
    /// </summary>
    public class Star
    {
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int OuterRadius { get; set; }
        public int InnerRadius { get; set; }
        public int Points { get; set; }

        // Build a star around the given center.
        public Star(int centerX, int centerY, int outerRadius, int innerRadius, int points = 5)
        {
            CenterX = centerX;
            CenterY = centerY;
            OuterRadius = outerRadius;
            InnerRadius = innerRadius;
            Points = points;
        }

        /// <summary>
        /// Compute the polygon vertices of the star, alternating between
        /// the outer and the inner radius.
        /// </summary>
        public Point[] GetPoints()
        {
            Point[] points = new Point[Points * 2];
            double angleStep = Math.PI * 2 / (Points * 2);
            double angle = -Math.PI / 2; // start pointing straight up

            for (int i = 0; i < Points * 2; i++)
            {
                // Even index -> outer vertex, odd index -> inner vertex.
                double radius = (i % 2 == 0) ? OuterRadius : InnerRadius;
                int x = CenterX + (int)(radius * Math.Cos(angle));
                int y = CenterY + (int)(radius * Math.Sin(angle));
                points[i] = new Point(x, y);
                angle += angleStep;
            }
            return points;
        }
    }
}
