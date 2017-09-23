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

        /// <summary>
        /// asin()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double asin(double x) => Math.Asin(x);

        /// <summary>
        /// acos()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double acos(double x) => Math.Acos(x);

        /// <summary>
        /// atan()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double atan(double x) => Math.Atan(x);

        /// <summary>
        /// atan2()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double atan2(double y, double x) => Math.Atan2(y, x);

        /// <summary>
        /// sinh()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double sinh(double x) => Math.Sinh(x);

        /// <summary>
        /// cosh()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double cosh(double x) => Math.Cosh(x);

        /// <summary>
        /// tanh()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double tanh(double x) => Math.Tanh(x);

        /// <summary>
        /// log()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double log(double x) => Math.Log(x);

        /// <summary>
        /// log10()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double log10(double x) => Math.Log10(x);

        /// <summary>
        /// pow()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double pow(double x, double y) => Math.Pow(x, y);

    }
#pragma warning restore IDE1006
}
