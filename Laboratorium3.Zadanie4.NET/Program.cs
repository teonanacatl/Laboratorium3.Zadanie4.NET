using System.Security.Cryptography;

namespace Laboratorium3.Zadanie4.NET
{
    class Program
    {
        static void Main()
        {
            var rsa = new RSACryptoServiceProvider(2048);
            var aes = Aes.Create();
            const string filePath = "test.txt";
        
            var data = File.ReadAllBytes(filePath);
            aes.GenerateKey();
            var encryptedAesKey = rsa.Encrypt(aes.Key, true);
            using var aesEncryptor = aes.CreateEncryptor();
            var encryptedData = aesEncryptor.TransformFinalBlock(data, 0, data.Length);
            File.WriteAllBytes(filePath + ".encrypted", encryptedData);
            File.WriteAllBytes(filePath + ".encryptedAesKey", encryptedAesKey);

            Console.WriteLine("Plik zaszyfrowany.");
        
            var encryptedDataFromFile = File.ReadAllBytes(filePath + ".encrypted");
            var encryptedAesKeyFromFile = File.ReadAllBytes(filePath + ".encryptedAesKey");
            var decryptedAesKey = rsa.Decrypt(encryptedAesKeyFromFile, true);
            aes.Key = decryptedAesKey;
            using var aesDecryptor = aes.CreateDecryptor();
            var decryptedData = aesDecryptor.TransformFinalBlock(encryptedDataFromFile, 0, encryptedDataFromFile.Length);
            File.WriteAllBytes(filePath + ".decrypted", decryptedData);

            Console.WriteLine("Plik odszyfrowany.");
        }
    }
}