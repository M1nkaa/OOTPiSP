using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;

class Program
{
    static string PrivateKeyPath = "private_key.xml";
    static string PublicKeyPath = "public_key.xml";

    static void Main(string[] args)
    {
        Console.Title = "Plugin Signer - Подпись плагинов";

        // === 1. Работа с ключами (генерируем только один раз) ===
        if (!File.Exists(PrivateKeyPath) || !File.Exists(PublicKeyPath))
        {
            Console.WriteLine("🔑 Генерация новой пары ключей...");
            GenerateKeys();
        }
        else
        {
            Console.WriteLine("✅ Используем существующие ключи");
        }

        // === 2. Определение DLL для подписи ===
        string dllPath = null;

        if (args.Length > 0 && File.Exists(args[0]))
        {
            dllPath = args[0];
        }
        else
        {
            Console.WriteLine("\n📌 Перетащите любой .dll плагин на это окно");
            Console.WriteLine("   или вставьте полный путь к файлу ниже:");
            Console.Write("> ");
            dllPath = Console.ReadLine()?.Trim('"', ' ');
        }

        if (string.IsNullOrEmpty(dllPath) || !File.Exists(dllPath))
        {
            Console.WriteLine("❌ DLL файл не найден!");
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
            return;
        }

        string sigPath = dllPath + ".sig";

        try
        {
            string privateKeyXml = File.ReadAllText(PrivateKeyPath);
            byte[] dllBytes = File.ReadAllBytes(dllPath);
            byte[] hash = SHA256.HashData(dllBytes);

            using RSA rsa = RSA.Create();
            rsa.FromXmlString(privateKeyXml);

            byte[] signature = rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            var sigInfo = new
            {
                Signature = Convert.ToBase64String(signature),
                SigningTime = DateTime.UtcNow,
                Expiration = DateTime.UtcNow.AddYears(1)
            };

            string json = JsonSerializer.Serialize(sigInfo, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(sigPath, json);

            Console.WriteLine("\n✅ Плагин успешно подписан!");
            Console.WriteLine($"Файл: {Path.GetFileName(dllPath)}");
            Console.WriteLine($"Подпись: {Path.GetFileName(sigPath)}");
            Console.WriteLine($"Действует до: {sigInfo.Expiration}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ошибка: {ex.Message}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static void GenerateKeys()
    {
        using RSA rsa = RSA.Create(2048);
        string privateKey = rsa.ToXmlString(true);
        string publicKey = rsa.ToXmlString(false);

        File.WriteAllText(PrivateKeyPath, privateKey);
        File.WriteAllText(PublicKeyPath, publicKey);

        Console.WriteLine("✅ Пара ключей успешно создана!");
        Console.WriteLine($"   {PrivateKeyPath}");
        Console.WriteLine($"   {PublicKeyPath}");
    }
}