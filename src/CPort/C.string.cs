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
        public static PChar strcpy(PChar s, PChar ct)
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
        public static PChar strncpy(PChar s, PChar ct, int n)
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
        public static PChar strcat(PChar s, PChar ct)
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
        public static PChar strncat(PChar s, PChar ct, int n)
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
        public static int strcmp(PChar cs, PChar ct)
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
        public static int strncmp(PChar cs, PChar ct, int n)
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
        public static PChar strchr(PChar cs, char c)
        {
            char ccs;
            while ((ccs = cs.Value) > 0)
            {
                if (ccs == c) return cs;
                cs++;
            }
            return new PChar();
        }

        /// <summary>
        /// strrchr()
        /// </summary>
        public static PChar strrchr(PChar cs, char c)
        {
            var result = new PChar();
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
        public static int strspn(PChar cs, PChar ct)
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
        public static int strcspn(PChar cs, PChar ct)
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
        public static PChar strpbrk(PChar cs, PChar ct)
        {
            char c;
            while ((c = cs.Value) > 0)
            {
                if (!strchr(ct, c).IsNull)
                    return cs;
                cs++;
            }
            return new PChar();
        }

        /// <summary>
        /// strstr()
        /// </summary>
        public static PChar strstr(PChar cs, PChar ct)
        {
            // If ct is empty then stop here because we never found an empty string
            if (cs.IsNull || ct.IsNull || ct.Value == 0) return new PChar();
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
            return new PChar();
        }

        /// <summary>
        /// strlen()
        /// </summary>
        public static int strlen(PChar cs)
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

        static PChar? _strtokCurrent = null;

        /// <summary>
        /// strtok()
        /// </summary>
        public static PChar strtok(PChar? s, PChar delimiters)
        {
            // If s is defined, reset the strtok process
            if (s != null && !s.Value.IsNull)
                _strtokCurrent = s;
            // If current strtok pointer is null, stop here
            if (_strtokCurrent == null || _strtokCurrent.Value.IsNull)
                return new PChar();
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
            return new PChar();
        }

    }

#pragma warning restore IDE1006
}
