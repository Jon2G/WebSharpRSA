using System.Numerics;
using System.Text;

namespace SharpRSALib
{
    public static class TextConverter
    {
        public static int ToNumber(char character)
        {
            return character;
        }

        public static BigInteger FromBase64(string b64)
        {
            byte[] bytes = Convert.FromBase64String(b64);
            return new BigInteger(bytes, true, true);
        }
        public static string ToBase64(BigInteger number)
        {
            byte[] bytes = number.ToByteArray(true, true);
            return Convert.ToBase64String(bytes);
        }
        public static BigInteger ToNumber(string text)
        {
            string b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
            byte[] bytes = Encoding.UTF8.GetBytes(b64);
            return new BigInteger(bytes, true, true);
        }
        public static string ToText(BigInteger number)
        {
            byte[] bytes = number.ToByteArray(true, true);
            string b64 = Encoding.UTF8.GetString(bytes);
            return Encoding.UTF8.GetString(Convert.FromBase64String(b64));
        }
    }
}
