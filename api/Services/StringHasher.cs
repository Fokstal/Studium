using System.Security.Cryptography;
using System.Text;

namespace api.Services
{
    public static class StringHasher
    {
        private static readonly string basicSalt = "basicSalt";

        public static string Generate(string inputLine)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(inputLine + basicSalt);
            byte[] hash = SHA512.HashData(bytes);

            return Convert.ToBase64String(hash);
        }

        public static string Generate(string inputLine, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(inputLine + salt);
            byte[] hash = SHA512.HashData(bytes);

            return Convert.ToBase64String(hash);
        }

        public static bool Verify(string verifyLine, string hashedLine) => Equals(Generate(verifyLine), hashedLine);
        public static bool Verify(string verifyLine, string hashedLine, string salt) => Equals(Generate(verifyLine, salt), hashedLine);
    }
}