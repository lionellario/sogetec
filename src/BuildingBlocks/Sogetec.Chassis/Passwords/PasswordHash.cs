using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Sogetec.Chassis.Passwords;

public static class PasswordHash
{
    private const int PBKDF2_KEY_LENGTH = 256 / 8; // 256 bits
    private const int SALT_SIZE = 128 / 8; // 128 bits

    private static byte[] GetHashedBytes(string password, byte[] salt)
    {
        // derive a 256-bit sub key (use HMAC SHA256 with 100,000 iterations)
        var hashed = KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100000,
            PBKDF2_KEY_LENGTH);

        var hashBytes = new byte[SALT_SIZE + PBKDF2_KEY_LENGTH];
        Buffer.BlockCopy(salt, 0, hashBytes, 0, SALT_SIZE);
        Buffer.BlockCopy(hashed, 0, hashBytes, SALT_SIZE, PBKDF2_KEY_LENGTH);

        return hashBytes;
    }

    public static string HashPassword(string password)
    {
        // Generate a 128-bit salt using a sequence of
        // cryptographically strong random bytes.
        var salt = RandomNumberGenerator.GetBytes(SALT_SIZE);

        var byts = GetHashedBytes(password, salt);
        return Convert.ToBase64String(byts);
    }

    public static bool VerifyPassword(string? password, string? hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword) || string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        var hashBytes = Convert.FromBase64String(hashedPassword);
        var salt = new byte[SALT_SIZE];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, SALT_SIZE);

        // derive a 256-bit sub key (use HMAC SHA256 with 100,000 iterations)
        var hashed = GetHashedBytes(password, salt);

        return hashed.SequenceEqual(Convert.FromBase64String(hashedPassword));
    }
}
