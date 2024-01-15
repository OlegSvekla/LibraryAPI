using System.Security.Cryptography;
using System.Text;

namespace LibraryAPI.BL.Helpers
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            return Hash(password);
        }

        public static bool Verify(string password, string inputHash)
        {
            var hash = Hash(password);

            return hash == inputHash;
        }

        private static string Hash(string input)
        {
            var bytes = Encoding.ASCII.GetBytes(input);

            using var hashAlgorithm = MD5.Create();

            return Encoding.ASCII.GetString(hashAlgorithm.ComputeHash(bytes));
        }
    }
}