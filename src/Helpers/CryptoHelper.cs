using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Librum.Helpers
{
    public static class CryptoHelper
    {
        public static string Sha1(string value)
        {
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(value));
            return string.Concat(hash.Select(b => b.ToString("x2")));
        }
    }
}