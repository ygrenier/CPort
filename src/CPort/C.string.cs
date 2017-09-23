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

        /// <summary>
        /// strchr()
        /// </summary>
        public static Pointer<char> strchr(Pointer<char> cs, char c)
        {
            char ccs;
            while ((ccs = cs.Value) > 0)
            {
                if (ccs == c) return cs;
                cs++;
            }
            return new Pointer<char>();
        }

        /// <summary>
        /// strrchr()
        /// </summary>
        public static Pointer<char> strrchr(Pointer<char> cs, char c)
        {
            var result = new Pointer<char>();
            char ccs;
            while ((ccs = cs.Value) > 0)
            {
                if (ccs == c) result = cs;
                cs++;
            }
            return result;
        }

        /// <summary>
        /// strspn()
        /// </summary>
        public static int strspn(Pointer<char> cs, Pointer<char> ct)
        {
            int cnt = 0;
            char c;
            while ((c = cs.Value) > 0)
            {
                if (strchr(ct, c).IsNull)
                    break;
                cnt++; cs++;
            }
            return cnt;
        }

        /// <summary>
        /// strcspn()
        /// </summary>
        public static int strcspn(Pointer<char> cs, Pointer<char> ct)
        {
            int cnt = 0;
            char c;
            while ((c = cs.Value) > 0)
            {
                if (!strchr(ct, c).IsNull)
                    break;
                cnt++; cs++;
            }
            return cnt;
        }

        /// <summary>
        /// strpbrk()
        /// </summary>
        public static Pointer<char> strpbrk(Pointer<char> cs, Pointer<char> ct)
        {
            char c;
            while ((c = cs.Value) > 0)
            {
                if (!strchr(ct, c).IsNull)
                    return cs;
                cs++;
            }
            return new Pointer<char>();
        }

        /// <summary>
        /// strstr()
        /// </summary>
        public static Pointer<char> strstr(Pointer<char> cs, Pointer<char> ct)
        {
            // If ct is empty then stop here because we never found an empty string
            if (ct.IsNull || ct.Value == 0) return new Pointer<char>();
            char c;
            while ((c = cs.Value) > 0)
            {
                var rs = cs; var rt = ct;
                char crs = '\xFFFF', crt = '\xFFFF';
                while ((crt = rt.Value) > 0 && (crs = rs.Value) > 0 && crs == crt)
                { rs++; rt++; }
                // We found ct only if crt==0
                if (crt == 0) return cs;
                cs++;
            }
            return new Pointer<char>();
        }

        /// <summary>
        /// strlen()
        /// </summary>
        public static int strlen(Pointer<char> cs)
        {
            if (cs.IsNull) return 0;
            int count = 0;
            while (cs.Value > 0)
            {
                count++;
                cs++;
            }
            return count;
        }

        static Pointer<char>? _strtokCurrent = null;

        /// <summary>
        /// strtok()
        /// </summary>
        public static Pointer<char> strtok(Pointer<char>? s, Pointer<char> delimiters)
        {
            // If s is defined, reset the strtok process
            if (s != null && !s.Value.IsNull)
                _strtokCurrent = s;
            // If current strtok pointer is null, stop here
            if (_strtokCurrent == null || _strtokCurrent.Value.IsNull)
                return new Pointer<char>();
            // Pass the delimiters
            var p = _strtokCurrent.Value;
            while (p.Value > 0 && !strchr(delimiters, p.Value).IsNull) p++;
            if (p.Value > 0)
            {
                var start = p;
                while (p.Value > 0 && strchr(delimiters, p.Value).IsNull) p++;
                p.Value = '\0';
                _strtokCurrent = p + 1;
                return start;
            }
            return new Pointer<char>();
        }

    }

#pragma warning restore IDE1006
}
