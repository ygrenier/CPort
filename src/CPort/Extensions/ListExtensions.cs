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

        /// <summary>
        /// Copy a list to a pointer
        /// </summary>
        public static void CopyTo<T>(this IList<T> source, Pointer<T> dest, int count = -1)
        {
            var sp = source.GetPointer();
            if (count < 0) count = source.Count;
            while (count-- > 0)
            {
                dest.Value = sp++;
                dest++;
            }
        }

    }
}
