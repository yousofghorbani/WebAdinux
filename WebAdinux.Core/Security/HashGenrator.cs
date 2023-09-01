using System.Security.Cryptography;
using System.Text;

namespace WebAdinux.Core.Security
{
    public static class HashGenerators
    {
        public static string TOMD5(this string password)
        {
            Byte[] mainBytes;
            Byte[] encodeBytes;

            MD5 md5;

            md5 = new MD5CryptoServiceProvider();

            mainBytes = ASCIIEncoding.Default.GetBytes(password);
            encodeBytes = md5.ComputeHash(mainBytes);

            return BitConverter.ToString(encodeBytes);
        }
    }
}
