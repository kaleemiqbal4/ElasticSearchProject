using System.Security.Cryptography;
using System.Text;

namespace ElasticSAearch.Common;

public static class HashSaltGenerator
{
    public static (string Hash, string Salt) HashPassword(this string password)
    {
        // Generate a random salt using RandomNumberGenerator
        byte[] saltBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        string salt = Convert.ToBase64String(saltBytes);

        // Hash the password with the salt
        using (var hmac = new HMACSHA256(saltBytes))
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = hmac.ComputeHash(passwordBytes);
            string hash = Convert.ToBase64String(hashBytes);

            return (hash, salt);
        }
    }

    public static bool VerifyPassword(this string password, string storedHash, string storedSalt)
    {
        // Convert the stored salt back to byte array
        byte[] saltBytes = Convert.FromBase64String(storedSalt);

        // Hash the provided password with the stored salt
        using (var hmac = new HMACSHA256(saltBytes))
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = hmac.ComputeHash(passwordBytes);
            string hash = Convert.ToBase64String(hashBytes);

            // Compare hashes
            return hash == storedHash;
        }
    }
}