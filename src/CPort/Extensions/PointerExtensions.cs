using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPort
{
    /// <summary>
    /// Extension methods for pointers
    /// </summary>
    public static class PointerExtensions
    {

        /// <summary>
        /// Extract the string from a pointer of char
        /// </summary>
        public static string GetString(this Pointer<char> pointer)
        {
            if (pointer.IsNull) return null;
            return new string(pointer.TakeWhile(c => c != '\0').ToArray());
        }

    }
}
