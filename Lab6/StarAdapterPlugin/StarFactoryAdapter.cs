using System;
using System.Drawing;
using Lab1.Shapes;
using PluginInterface;

namespace StarAdapterPlugin
{
    // ============================================================
    //  ADAPTER PATTERN  --  the core Adapter (part 2 of 3).
    //
    //  Target   : Lab1.Shapes.IShapeFactory          (what our host expects)
    //  Adaptee  : PluginInterface.IShapeFactoryPlugin (what the partner gives)
    //  Adapter  : StarFactoryAdapter                  (this class)
    //
    //  The two interfaces are incompatible in three ways:
    //    1. Creation protocol  - host: one CreateShape(point, params...) call;
    //                            partner: AddClick -> IsReady -> CreateShape().
    //    2. Return type        - host returns Shape; partner returns object.
    //    3. Parameter passing  - host passes width/height/color as arguments;
    //                            partner asks the user via its own dialogs.
    //
    //  The host loads this class through the normal plugin mechanism
    //  (it implements IShapeFactory), so the partner's feature appears
    //  in the program WITHOUT changing either side's source code.
    // ============================================================

    /// <summary>
    /// Adapts the partner's <see cref="IShapeFactoryPlugin"/> so the host
    /// can use it as an ordinary <see cref="IShapeFactory"/>.
    /// </summary>
    public class StarFactoryAdapter : IShapeFactory
    {
        // The Adaptee, referenced only through the partner's interface.
        private readonly IShapeFactoryPlugin _partnerFactory;

        // Parameterless constructor: required because the host creates
        // factories via Activator.CreateInstance during plugin loading.
        public StarFactoryAdapter()
        {
            // The adapter owns and drives a concrete partner factory.
            _partnerFactory = new StarPlugin.StarFactory();
        }

        // Name shown in the host's shape-type combo box. The suffix makes
        // it obvious in the UI that this entry comes through the adapter.
        public string ShapeName => $"{_partnerFactory.ShapeName} (Partner Adapter)";

        /// <summary>
        /// Translate the host's single-call request into the partner's
        /// multi-step protocol and wrap the result as a host Shape.
        /// </summary>
        public Shape CreateShape(Point startPoint, params object[] parameters)
        {
            // --- Step 1: drive the partner's click-accumulation protocol ---
            _partnerFactory.Reset();                       // start clean
            _partnerFactory.AddClick(startPoint.X, startPoint.Y);

            if (!_partnerFactory.IsReady())
                throw new InvalidOperationException(
                    "Partner factory still needs more clicks to build a shape.");

            // --- Step 2: let the partner build its own object ---
            // (the partner may pop up its own InputBox dialogs here)
            object created = _partnerFactory.CreateShape();

            if (created is not StarPlugin.Star partnerStar)
                throw new InvalidCastException(
                    "Partner factory returned an unexpected object type.");

            // --- Step 3: adapt the result to a host-compatible Shape ---
            Color color = ExtractColor(parameters);
            return new AdaptedStar(partnerStar, color);
        }

        /// <summary>
        /// Pick the colour chosen in the host UI out of the loosely-typed
        /// parameter list. The host may pass (width,height,color),
        /// (width,color) or just (color); we simply take the first Color.
        /// </summary>
        private static Color ExtractColor(object[] parameters)
        {
            foreach (object p in parameters)
                if (p is Color c)
                    return c;

            // Sensible fallback if the host supplied no colour at all.
            return Color.Black;
        }
    }
}
