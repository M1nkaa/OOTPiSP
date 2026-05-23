using System;

namespace PluginInterface
{
    // ============================================================
    //  PARTNER'S CONTRACT  --  this is the "Adaptee" interface.
    //  It was authored by another student and shipped inside
    //  PluginInterface.dll. We reproduce it here as source so the
    //  adapter plugin compiles into a single self-contained DLL.
    //  This interface MUST NOT be modified: the whole point of the
    //  Adapter pattern is that we cannot change the partner's code.
    // ============================================================

    /// <summary>
    /// Shape-factory contract used by the PARTNER application.
    /// It is intentionally incompatible with our own IShapeFactory:
    /// it creates a shape through a multi-step "click accumulation"
    /// protocol instead of a single CreateShape() call.
    /// </summary>
    public interface IShapeFactoryPlugin
    {
        // Display name of the shape this factory produces.
        string ShapeName { get; }

        // How many mouse clicks the factory needs before it is ready.
        int NeededClicks { get; }

        // Feed one mouse click (in screen coordinates) to the factory.
        void AddClick(int x, int y);

        // True once enough clicks have been collected to build a shape.
        bool IsReady();

        // Build the shape. Returns a loosely-typed object because the
        // partner application has no common shape base class.
        object CreateShape();

        // Discard any accumulated clicks so the factory can be reused.
        void Reset();

        // Return the clicks collected so far.
        (int X, int Y)[] GetCoordinates();
    }
}
