using System.Security.Cryptography;
using System.Text;

namespace GoldenHeart.Models
{
    public class Hash
    {
        // Функция хеширования
        public static string GetHashString(string s)
        {
            byte[] hash = Encoding.ASCII.GetBytes(s);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string result = "";
            foreach (var b in hashenc)
                result += b.ToString("x2");
            return result;
        }
    }
}