using System.Security.Cryptography;
using System.Text;
namespace UserManagement.Core.Utils;


public static class AesEncryption
{
    private const int KeySize = 256;
    private const int Iterations = 10000;

    public static byte[] Encrypt(string plaintext, string password)
    {
        byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

        using Aes aes = Aes.Create();
        aes.Key = GenerateKey(password);
        aes.IV = GenerateIV(password);

        using MemoryStream ms = new();
        using (CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
            cs.Write(plaintextBytes, 0, plaintextBytes.Length);
        }

        byte[] ciphertext = ms.ToArray();
        return ciphertext;
    }

    public static string Decrypt(byte[] ciphertext, string password)
    {
        using Aes aes = Aes.Create();
        aes.Key = GenerateKey(password);
        aes.IV = GenerateIV(password);

        using MemoryStream ms = new();
        using (CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cs.Write(ciphertext, 0, ciphertext.Length);
        }

        byte[] plaintextBytes = ms.ToArray();
        string plaintext = Encoding.UTF8.GetString(plaintextBytes);
        return plaintext;
    }

    private static byte[] GenerateKey(string password)
    {
        // Generate a key from the password using PBKDF2
        byte[] salt = Encoding.UTF8.GetBytes("AesEncryption");
        using var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        return deriveBytes.GetBytes(KeySize / 8);
    }

    private static byte[] GenerateIV(string password)
    {
        // Generate an IV from the password using PBKDF2
        byte[] salt = Encoding.UTF8.GetBytes("AesEncryptionIV");
        using var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        return deriveBytes.GetBytes(16);
    }
}