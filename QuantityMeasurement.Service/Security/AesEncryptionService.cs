using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
namespace QuantityMeasurement.Service.Security
{
    // UC18: AES-256 encryption and decryption service.
    public class AesEncryptionService
    {
        private readonly byte[] key;
        public AesEncryptionService(IConfiguration configuration)
        {
            string aesKey = configuration["Encryption:AesKey"]!;
            key = Convert.FromBase64String(aesKey);
        }
        // Constructor for testing with direct key
        public AesEncryptionService(byte[] directKey)
        {
            key = directKey;
        }
        // Encrypt plaintext using AES-256
        public string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();
                byte[] iv = aes.IV;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, iv);
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                // Prepend IV to ciphertext for decryption
                byte[] result = new byte[iv.Length + encryptedBytes.Length];
                Array.Copy(iv, 0, result, 0, iv.Length);
                Array.Copy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);
                return Convert.ToBase64String(result);
            }
        }
        // Decrypt ciphertext back to plaintext
        public string Decrypt(string cipherText)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                // Extract IV from the first 16 bytes
                byte[] iv = new byte[16];
                Array.Copy(fullCipher, 0, iv, 0, 16);
                aes.IV = iv;
                // Extract ciphertext after the IV
                byte[] cipherBytes = new byte[fullCipher.Length - 16];
                Array.Copy(fullCipher, 16, cipherBytes, 0, cipherBytes.Length);
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
