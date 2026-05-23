using System;

namespace Lab1.Shapes
{
    /// <summary>
    /// Interface for file processor plugins.
    /// Each plugin can transform data before saving and after loading.
    /// </summary>
    public interface IFileProcessorPlugin
    {
        /// <summary>
        /// Human-readable name of this plugin, shown in Settings menu.
        /// </summary>
        string PluginName { get; }

        /// <summary>
        /// Process data before saving to file (e.g. add checksum, compress, encrypt).
        /// </summary>
        /// <param name="data">Raw serialized data as string.</param>
        /// <returns>Processed data string to write to file.</returns>
        string ProcessBeforeSave(string data);

        /// <summary>
        /// Process data after loading from file (e.g. verify checksum, decompress, decrypt).
        /// Throws InvalidDataException if data is invalid (e.g. checksum mismatch).
        /// </summary>
        /// <param name="data">Raw data string read from file.</param>
        /// <returns>Clean data string for deserialization.</returns>
        string ProcessAfterLoad(string data);
    }
}
