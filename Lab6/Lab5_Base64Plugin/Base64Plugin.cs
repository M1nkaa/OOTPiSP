using System;
using System.IO;
using System.Text;
using Lab1.Shapes;

namespace Lab5_Base64Plugin
{
    /// <summary>
    /// File processor plugin that encodes the data as Base64 before saving
    /// and decodes it after loading.
    /// This obfuscates the file content from casual inspection.
    /// </summary>
    public class Base64Plugin : IFileProcessorPlugin
    {
        /// <summary>
        /// Name shown in the Settings menu.
        /// </summary>
        public string PluginName => "Base64 Encoding";

        /// <summary>
        /// Encode the data as Base64 before saving to file.
        /// </summary>
        public string ProcessBeforeSave(string data)
        {
            // Convert string to bytes, then to Base64
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Decode the Base64 data after loading from file.
        /// Throws InvalidDataException if the data is not valid Base64.
        /// </summary>
        public string ProcessAfterLoad(string data)
        {
            try
            {
                // Decode Base64 back to original string
                byte[] bytes = Convert.FromBase64String(data.Trim());
                return Encoding.UTF8.GetString(bytes);
            }
            catch (FormatException ex)
            {
                throw new InvalidDataException(
                    "Failed to decode Base64 data. The file may be corrupted or was not encoded with this plugin.",
                    ex);
            }
        }
    }
}
