using System;

namespace Lab1.Patterns
{
    // ============================================================
    //  COMMAND PATTERN  --  the command interface.
    //
    //  Each user action that changes the canvas is turned into an
    //  object implementing this interface. Because every command
    //  knows how to Execute() itself AND how to Undo() itself, the
    //  form can keep a history stack and offer an Undo button.
    // ============================================================

    /// <summary>
    /// A reversible user action.
    /// </summary>
    public interface ICommand
    {
        /// <summary>Perform the action.</summary>
        void Execute();

        /// <summary>Revert the action, restoring the previous state.</summary>
        void Undo();

        /// <summary>Short human-readable description (handy for logging/UI).</summary>
        string Description { get; }
    }
}
