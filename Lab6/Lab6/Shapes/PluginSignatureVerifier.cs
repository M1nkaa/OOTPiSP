using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;

namespace Lab1.Shapes
{
    // Verifies plugin signature using RSA
    public static class PluginSignatureVerifier
    {
        // Public key (will be created automatically on first signature)
        private static string PublicKeyXml = "";

        // Signature data structure
        public class PluginSignature
        {
            public string Signature { get; set; } = "";
            public DateTime SigningTime { get; set; }
            public DateTime Expiration { get; set; }
        }

        // Verifies plugin DLL using .sig file
        public static bool VerifyPlugin(string dllPath, out string error)
        {
            error = null;

            try
            {
                // Look for .sig file next to DLL
                string sigPath = dllPath + ".sig";
                if (!File.Exists(sigPath))
                {
                    error = "Missing .sig file";
                    return false;
                }

                // Look for public_key.xml
                string pluginsDir = Path.GetDirectoryName(dllPath) ?? "";
                string keyPath = Path.Combine(pluginsDir, "public_key.xml");

                if (!File.Exists(keyPath))
                {
                    keyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public_key.xml");
                }

                if (!File.Exists(keyPath))
                {
                    error = $"public_key.xml not found. Checked: {keyPath}";
                    return false;
                }

                // Load key and signature
                string publicKeyXml = File.ReadAllText(keyPath);
                string sigJson = File.ReadAllText(sigPath);

                var sigInfo = JsonSerializer.Deserialize<PluginSignature>(sigJson);
                if (sigInfo == null || string.IsNullOrEmpty(sigInfo.Signature))
                {
                    error = "Corrupted .sig file";
                    return false;
                }

                // Check expiration
                if (DateTime.UtcNow > sigInfo.Expiration)
                {
                    error = $"Plugin expired (until {sigInfo.Expiration})";
                    return false;
                }

                // === Debug info ===
                Console.WriteLine($"[DEBUG] Verifying: {Path.GetFileName(dllPath)}");
                Console.WriteLine($"[DEBUG] Key found: {keyPath}");
                Console.WriteLine($"[DEBUG] Expiration: {sigInfo.Expiration}");

                // Compute hash and verify signature
                byte[] dllBytes = File.ReadAllBytes(dllPath);
                byte[] hash = SHA256.HashData(dllBytes);
                byte[] signatureBytes = Convert.FromBase64String(sigInfo.Signature);

                using RSA rsa = RSA.Create();
                rsa.FromXmlString(publicKeyXml);

                bool isValid = rsa.VerifyHash(hash, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                if (!isValid)
                    error = "Signature verification failed (key or signature mismatch)";
                else
                    error = "Signature is valid";

                Console.WriteLine($"[DEBUG] Verification result: {isValid}");
                // ========================

                return isValid;
            }
            catch (Exception ex)
            {
                error = $"Error: {ex.Message}";
                Console.WriteLine($"[DEBUG] Exception: {ex.Message}");
                return false;
            }
        }
    }
}