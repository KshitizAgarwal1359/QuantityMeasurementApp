using System.Security.Cryptography;
using System.Text;
namespace QuantityMeasurement.Service.Security
{
    // UC18: SHA-256 password hasher with random salt.
    public class PasswordHasher
    {
        // Generate a cryptographically random 32-byte salt
        public string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        // Hash password with salt using SHA-256
        public string HashPassword(string password, string salt)
        {
            string combined = salt + password;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(combined);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
        // Verify password against stored hash
        public bool VerifyPassword(string password, string salt, string storedHash)
        {
            string computedHash = HashPassword(password, salt);
            return computedHash == storedHash;
        }
    }
}
