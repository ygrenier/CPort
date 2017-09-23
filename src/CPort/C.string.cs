using System;
using System.Collections.Generic;
using System.Text;

namespace CPort
{
    /// <summary>
    /// string.h
    /// </summary>
    static partial class C
    {
        /// <summary>
        /// strcpy()
        /// </summary>
        /// <param name="s">Destination string</param>
        /// <param name="ct">Source string</param>
        /// <returns>returns <paramref name="s"/></returns>
        public static Pointer<char> strcpy(Pointer<char> s, Pointer<char> ct)
        {
            var sd = s;
            var ss = ct;
            char c;
            do
            {
                c = ss++;
                sd.Value = c;
                sd++;
            } while (c != '\0');
            return s;
        }
        /// <summary>
        /// strncpy()
        /// </summary>
        /// <param name="s">Destination string</param>
        /// <param name="ct">Source string</param>
        /// <param name="n">Count of characters to copy</param>
        /// <returns>returns <paramref name="s"/></returns>
        public static Pointer<char> strncpy(Pointer<char> s, Pointer<char> ct, int n)
        {
            var sd = s;
            var ss = ct;
            int cnt = n;
            char c;
            do
            {
                c = ss++;
                sd.Value = c;
                sd++;
            } while (--n > 0 && c != '\0');
            while (n-- > 0)
            {
                sd.Value = '\0';
                sd++;
            }
            return s;
        }
    }
}
