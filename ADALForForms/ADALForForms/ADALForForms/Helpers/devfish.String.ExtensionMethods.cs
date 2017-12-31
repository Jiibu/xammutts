using System;
using System.Collections.Generic;
using System.Text;

namespace devfish.String
{
    public static class ExtensionMethods
    {
        public static string TakeUpTo(this string s, int thisMany)
        {
            return ( s.Length <= thisMany ? s : s.Substring(0, thisMany) );
        }
    }
}
