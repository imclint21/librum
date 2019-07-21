using System.Text.RegularExpressions;

namespace Librum.Helpers
{
    public static class StringHelper
    {
        public static int WordsCount(this string value)
        {
            MatchCollection collection = Regex.Matches(value, @"[\S]+");
            return collection.Count;
        }
    }
}