using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Librum.Helpers
{
    public class CryptoHelper
    {
        static static string Sha1(string value)
        {
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(value));
            return string.Concat(hash.Select(b => b.ToString("x2")));
        }
    }
}