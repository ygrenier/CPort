using System;
using System.Collections.Generic;
using System.Text;

namespace CPort
{
    /// <summary>
    /// Extensions of string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Check if a string contains a char
        /// </summary>
        public static bool Contains(this string s, char c)
        {
            return s?.IndexOf(c) >= 0;
        }

        /// <summary>
        /// Get a pointer of char from a string
        /// </summary>
        public static Pointer<char> GetPointer(this string s)
        {
            if (s == null) return new Pointer<char>(null);
            var arr = new char[s.Length + 1];   // Add a '\0'
            s.CopyTo(0, arr, 0, s.Length);
            arr[arr.Length - 1] = '\0';
            return new Pointer<char>(arr);
        }

    }
}
