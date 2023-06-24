using System;

namespace SoftblueLib2
{
    public static class StringUtils
    {
        public static string Capitalize(string s)
        {
            if (s == null)
            {
                return null;
            }

            if (s.Length == 0)
            {
                return s;
            }

            char c = char.ToUpper(s[0]);
            return c + s.Substring(1);
        }
    }
}
