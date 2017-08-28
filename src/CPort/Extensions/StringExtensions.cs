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
    }
}
