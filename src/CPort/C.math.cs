using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CPort
{
#pragma warning disable IDE1006
    /// <summary>
    /// math.h functions
    /// </summary>
    static partial class C
    {
        /// <summary>
        /// sin()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double sin(double x) => Math.Sin(x);

        /// <summary>
        /// cos()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double cos(double x) => Math.Cos(x);

        /// <summary>
        /// tan()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double tan(double x) => Math.Tan(x);
    }
#pragma warning restore IDE1006
}
