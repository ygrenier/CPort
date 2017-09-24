using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CPort
{
#pragma warning disable IDE1006

    /// <summary>
    /// stdlib.h functions
    /// </summary>
    static partial class C
    {
        static Pointer<char> ReadNumber(Pointer<char> p)
        {
            while (isdigit(p.Value)) p++;
            if (p.Value == '.')
            {
                p++;
                while (isdigit(p.Value)) p++;
                if (tolower(p.Value) == 'e')
                {
                    p++;
                    if (p.Value == '+' || p.Value == '-') p++;
                    while (isdigit(p.Value)) p++;
                }
            }
            return p;
        }
        static Pointer<char> StartsWith(Pointer<char> s, string value)
        {
            //if (s.IsNull || s.Value == '\0' || string.IsNullOrEmpty(value)) return NULL;
            foreach (char c in value)
            {
                if (tolower(s.Value) != tolower(c)) return NULL;
                s++;
            }
            return s;
        }

        /// <summary>
        /// strtod()
        /// </summary>
        public static double strtod(Pointer<char> s, out Pointer<char> endp)
        {
            endp = NULL;
            if (s.IsNull) return 0;
            // Trim start
            var p = s;
            while (p.Value != '\0' && isspace(p.Value)) p++;
            if (p.Value == '\0') { endp = p; return 0; }
            var ps = p;
            string str = null;
            bool positive = true;
            // + | -
            if (p.Value == '+') p++;
            else if (p.Value == '-')
            {
                positive = false;
                p++;
            }
            // 0 -> number or 0X
            if (p.Value == '0')
            {
                p++;
                if (tolower(p.Value) == 'x')
                {
                    p++;
                    var psh = p;
                    while (isxdigit(p.Value)) p++;
                    str = new string(psh.Take(p.Index - psh.Index).ToArray());
                    if (long.TryParse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out long resi))
                    {
                        endp = p;
                        return positive ? resi : -resi;
                    }
                    endp = ps + 1;
                    return 0;
                }
                p = ReadNumber(p);
                str = new string(ps.Take(p.Index - ps.Index).ToArray());
                //if (double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out double resd))
                //{
                //    endp = p;
                //    return resd;
                //}
                //endp = ps + 1;
                //return 0;
                endp = p;
                return double.Parse(str, NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            // . | 1..9 -> number
            else if (p.Value == '.' || isdigit(p.Value))
            {
                p = ReadNumber(p);
                str = new string(ps.Take(p.Index - ps.Index).ToArray());
                if (double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out double resd))
                {
                    endp = p;
                    return resd;
                }
                endp = ps;
                return 0;
            }
            // INF | INFINITY
            else if (tolower(p.Value) == 'i')
            {
                var pi = StartsWith(p, "INFINITY");
                if (!pi.IsNull)
                {
                    endp = pi;
                    return positive ? double.PositiveInfinity : double.NegativeInfinity;
                }
                pi = StartsWith(p, "INF");
                if (!pi.IsNull)
                {
                    endp = pi;
                    return positive ? double.PositiveInfinity : double.NegativeInfinity;
                }
                endp = ps;
                return 0;
            }
            // NAN
            else if (tolower(p.Value) == 'n')
            {
                p = StartsWith(ps, "NAN");
                if (!p.IsNull)
                {
                    endp = p;
                    return double.NaN;
                }
                endp = ps;
                return 0;
            }
            endp = ps;
            return 0;
        }

    }

#pragma warning restore IDE1006
}
