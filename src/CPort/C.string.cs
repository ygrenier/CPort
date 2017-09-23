using System;
using System.Collections.Generic;
using System.Text;

namespace CPort
{
#pragma warning disable IDE1006

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

        /// <summary>
        /// strcat()
        /// </summary>
        /// <param name="s">Destination string</param>
        /// <param name="ct">Source string</param>
        /// <returns>returns <paramref name="s"/></returns>
        public static Pointer<char> strcat(Pointer<char> s, Pointer<char> ct)
        {
            var sd = s;
            // Go to end
            while (sd.Value != '\0') sd++;
            // Copy to end
            strcpy(sd, ct);
            return s;
        }

        /// <summary>
        /// strncat()
        /// </summary>
        /// <param name="s">Destination string</param>
        /// <param name="ct">Source string</param>
        /// <param name="n">Count of characters to concat</param>
        /// <returns>returns <paramref name="s"/></returns>
        public static Pointer<char> strncat(Pointer<char> s, Pointer<char> ct, int n)
        {
            var sd = s;
            // Go to end
            while (sd.Value != '\0') sd++;
            // Copy to end
            strncpy(sd, ct, n);
            sd[n] = '\0';
            return s;
        }

        /// <summary>
        /// strcmp()
        /// </summary>
        public static int strcmp(Pointer<char> cs, Pointer<char> ct)
        {
            char ccs, cct;
            do
            {
                ccs = cs.Value;
                cct = ct.Value;
                cs++; ct++;
                int r = ccs - cct;
                if (r != 0) return r;
            } while (ccs > 0 && cct > 0);
            return 0;
        }

        /// <summary>
        /// strncmp()
        /// </summary>
        public static int strncmp(Pointer<char> cs, Pointer<char> ct, int n)
        {
            char ccs, cct;
            do
            {
                ccs = cs.Value;
                cct = ct.Value;
                cs++; ct++;
                int r = ccs - cct;
                if (r != 0) return r;
            } while (--n > 0 && ccs > 0 && cct > 0);
            return 0;
        }

    }

#pragma warning restore IDE1006
}
