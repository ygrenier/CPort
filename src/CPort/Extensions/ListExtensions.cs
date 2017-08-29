using System;
using System.Collections.Generic;
using System.Text;

namespace CPort
{
    /// <summary>
    /// Extensions of list
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Create a pointer from a list
        /// </summary>
        public static Pointer<T> GetPointer<T>(this IList<T> source)
        {
            return new Pointer<T>(source);
        }
    }
}
