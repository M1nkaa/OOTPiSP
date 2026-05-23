using System;
using System.IO;
using System.Text;
using Lab1.Shapes;

namespace Lab5_EncryptPlugin
{
    /// <summary>
    /// File processor plugin that applies a simple XOR cipher to the data
    /// before saving and after loading. Provides lightweight obfuscation.
    /// The same key is used for both encoding and decoding (symmetric).
    /// </summary>
    public class XorEncryptPlugin : IFileProcessorPlugin
    {
        // XOR key used for encryption/decryption
        // In a real application this should be configurable or stored securely
        private const string XorKey = "Lab5SecretKey2024";

        /// <summary>
        /// Name shown in the Settings menu.
        /// </summary>
        public string PluginName => "XOR Encryption";

        /// <summary>
        /// Encrypt the data using XOR cipher before saving.
        /// Result is stored as Base64 to keep it printable.
        /// </summary>
        public string ProcessBeforeSave(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] keyBytes = Encoding.UTF8.GetBytes(XorKey);

            // XOR each byte of data with corresponding key byte (cycling the key)
            byte[] result = new byte[dataBytes.Length];
            for (int i = 0; i < dataBytes.Length; i++)
                result[i] = (byte)(dataBytes[i] ^ keyBytes[i % keyBytes.Length]);

            // Store as Base64 so the file remains a text file
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// Decrypt the data using XOR cipher after loading.
        /// XOR is its own inverse, so the same operation decrypts.
        /// </summary>
        public string ProcessAfterLoad(string data)
        {
            byte[] encryptedBytes;
            try
            {
                encryptedBytes = Convert.FromBase64String(data.Trim());
            }
            catch (FormatException ex)
            {
                throw new InvalidDataException(
                    "Failed to parse encrypted data. The file may be corrupted.", ex);
            }

            byte[] keyBytes = Encoding.UTF8.GetBytes(XorKey);

            // Decrypt: XOR again with the same key
            byte[] result = new byte[encryptedBytes.Length];
            for (int i = 0; i < encryptedBytes.Length; i++)
                result[i] = (byte)(encryptedBytes[i] ^ keyBytes[i % keyBytes.Length]);

            return Encoding.UTF8.GetString(result);
        }
    }
}
