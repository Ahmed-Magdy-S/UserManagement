using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using UserManagement.Core.IdentityEntities;

namespace UserManagement.Core.Utils;

public class Sha512PasswordHasher : IPasswordHasher<AppUser>
{
    public string HashPassword(AppUser user, string password)
    {
        byte[] salt = GenerateSalt();
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltedPasswordBytes = CombineBytes(passwordBytes, salt);
        byte[] hashBytes = SHA512.HashData(saltedPasswordBytes);

        byte[] saltedHashBytes = CombineBytes(hashBytes, salt);
        return Convert.ToBase64String(saltedHashBytes);
    }

    public PasswordVerificationResult VerifyHashedPassword(AppUser user, string hashedPassword, string providedPassword)
    {
        byte[] saltedHashBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = ExtractSalt(saltedHashBytes);
        byte[] hashBytes = ExtractHash(saltedHashBytes);

        byte[] providedPasswordBytes = Encoding.UTF8.GetBytes(providedPassword);
        byte[] saltedProvidedPasswordBytes = CombineBytes(providedPasswordBytes, salt);
        byte[] providedHashBytes = SHA512.HashData(saltedProvidedPasswordBytes);

        if (AreByteArraysEqual(hashBytes, providedHashBytes))
        {
            return PasswordVerificationResult.Success;
        }
        else
        {
            return PasswordVerificationResult.Failed;
        }
    }

    private static byte[] GenerateSalt()
    {
        const int SaltSize = 16;
        byte[] salt = new byte[SaltSize];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        return salt;
    }

    private static byte[] CombineBytes(byte[] left, byte[] right)
    {
        byte[] combined = new byte[left.Length + right.Length];
        Buffer.BlockCopy(left, 0, combined, 0, left.Length);
        Buffer.BlockCopy(right, 0, combined, left.Length, right.Length);
        return combined;
    }

    private static byte[] ExtractSalt(byte[] saltedHashBytes)
    {
        const int SaltSize = 16;
        byte[] salt = new byte[SaltSize];
        Buffer.BlockCopy(saltedHashBytes, saltedHashBytes.Length - SaltSize, salt, 0, SaltSize);
        return salt;
    }

    private static byte[] ExtractHash(byte[] saltedHashBytes)
    {
        byte[] hash = new byte[saltedHashBytes.Length - 16];
        Buffer.BlockCopy(saltedHashBytes, 0, hash, 0, saltedHashBytes.Length - 16);
        return hash;
    }

    private static bool AreByteArraysEqual(byte[] a, byte[] b)
    {
        if (a == null || b == null || a.Length != b.Length)
        {
            return false;
        }

        bool areEqual = true;

        for (int i = 0; i < a.Length; i++)
        {
            areEqual &= (a[i] == b[i]);
        }

        return areEqual;
    }
}