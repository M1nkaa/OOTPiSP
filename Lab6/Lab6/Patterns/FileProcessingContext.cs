using System;
using Lab1.Shapes;

namespace Lab1.Patterns
{
    // ============================================================
    //  STRATEGY PATTERN  --  the context.
    //
    //  Roles:
    //    Strategy interface : Lab1.Shapes.IFileProcessorPlugin
    //    Concrete strategies: Base64 / Checksum / XOR plugins
    //    Context            : FileProcessingContext  (this class)
    //    Client             : the Figures form
    //
    //  The way file data is transformed on save/load (encode,
    //  checksum, encrypt or nothing) is an algorithm that varies
    //  independently of the save/load workflow itself. The Strategy
    //  pattern keeps that algorithm behind one interface and lets
    //  the client swap it at run time (via the Settings menu)
    //  without any if/else chains in the form.
    // ============================================================

    /// <summary>
    /// Holds the file-processing strategy currently chosen by the user
    /// and applies it when shapes are saved or loaded.
    /// </summary>
    public class FileProcessingContext
    {
        // The active strategy. null means "no processing".
        private IFileProcessorPlugin? _strategy;

        /// <summary>The strategy in use, or null if none is selected.</summary>
        public IFileProcessorPlugin? CurrentStrategy => _strategy;

        /// <summary>Display name of the active strategy.</summary>
        public string StrategyName => _strategy?.PluginName ?? "None";

        /// <summary>Select a new strategy (pass null to disable processing).</summary>
        public void SetStrategy(IFileProcessorPlugin? strategy) => _strategy = strategy;

        /// <summary>
        /// Apply the strategy to data that is about to be written to file.
        /// Returns the data unchanged when no strategy is selected.
        /// </summary>
        public string ApplyOnSave(string data)
            => _strategy != null ? _strategy.ProcessBeforeSave(data) : data;

        /// <summary>
        /// Apply the strategy to data that has just been read from file.
        /// Returns the data unchanged when no strategy is selected.
        /// </summary>
        public string ApplyOnLoad(string data)
            => _strategy != null ? _strategy.ProcessAfterLoad(data) : data;
    }
}
