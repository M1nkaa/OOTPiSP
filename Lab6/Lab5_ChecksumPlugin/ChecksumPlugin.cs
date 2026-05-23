using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Lab1.Shapes;

namespace Lab5_ChecksumPlugin
{
    /// <summary>
    /// File processor plugin that computes and appends a SHA-256 checksum
    /// to the serialized data before saving, and verifies it after loading.
    /// This ensures file integrity and detects corruption or tampering.
    /// </summary>
    public class ChecksumPlugin : IFileProcessorPlugin
    {
        // Separator line between data and checksum in the file
        private const string ChecksumSeparator = "---CHECKSUM---";

        /// <summary>
        /// Name shown in the Settings menu.
        /// </summary>
        public string PluginName => "Checksum (SHA-256)";

        /// <summary>
        /// Appends a SHA-256 checksum to the data before saving.
        /// The resulting format is:
        ///   [original json data]
        ///   ---CHECKSUM---
        ///   [hex checksum]
        /// </summary>
        public string ProcessBeforeSave(string data)
        {
            // Compute SHA-256 hash of the original JSON data
            string checksum = ComputeSha256(data);

            // Append separator and checksum
            return data + Environment.NewLine + ChecksumSeparator + Environment.NewLine + checksum;
        }

        /// <summary>
        /// Verifies the SHA-256 checksum after loading.
        /// Throws InvalidDataException if the checksum does not match.
        /// Returns the original data (without the checksum section) on success.
        /// </summary>
        public string ProcessAfterLoad(string data)
        {
            // Find the separator that divides data from checksum
            int separatorIndex = data.IndexOf(ChecksumSeparator, StringComparison.Ordinal);
            if (separatorIndex < 0)
                throw new InvalidDataException(
                    "Checksum not found in file. The file may be corrupted or was saved without the checksum plugin.");

            // Extract original data and stored checksum
            string originalData = data.Substring(0, separatorIndex).TrimEnd();
            string storedChecksum = data.Substring(separatorIndex + ChecksumSeparator.Length).Trim();

            // Recompute checksum from the extracted data
            string computedChecksum = ComputeSha256(originalData);

            // Compare — reject if mismatch
            if (!string.Equals(storedChecksum, computedChecksum, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException(
                    $"Checksum mismatch! The file may be corrupted or tampered with.\n" +
                    $"Expected: {computedChecksum}\n" +
                    $"Found:    {storedChecksum}");

            return originalData;
        }

        /// <summary>
        /// Compute SHA-256 hash of the given string and return it as a lowercase hex string.
        /// </summary>
        private static string ComputeSha256(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = SHA256.HashData(bytes);

            // Convert byte array to lowercase hex string
            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }
    }
}
