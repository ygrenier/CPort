/**
 * Some functions are imported from the project https://github.com/MachineCognitis/C.math.NET
 */

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

        /// <summary>
        /// sqrt()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double sqrt(double x) => Math.Sqrt(x);

        /// <summary>
        /// ceil()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double ceil(double x) => Math.Ceiling(x);

        /// <summary>
        /// floor()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double floor(double x) => Math.Floor(x);

        /// <summary>
        /// fabs()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double fabs(double x) => Math.Abs(x);

        /// <summary>
        /// ldexp()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double ldexp(double x, int n) => x * Math.Pow(2, n);

        /// <summary>
        /// modf()
        /// </summary>
        public static double modf(double x, ref double ip)
        {
            ip = Math.Truncate(x);
            return x - ip;
        }

        /// <summary>
        /// fmod()
        /// </summary>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static double fmod(double x, double y) => x % y;

        #region Import from https://github.com/MachineCognitis/C.math.NET

        #region "Properties of floating-point types."

        /// <summary>
        /// The exponent bias of a <see cref="Double"/>, i.e. value to subtract from the stored exponent to get the real exponent (<c>1023</c>).
        /// </summary>
        public const int DBL_EXP_BIAS = 1023;

        /// <summary>
        /// The number of bits in the exponent of a <see cref="Double"/> (<c>11</c>).
        /// </summary>
        public const int DBL_EXP_BITS = 11;

        /// <summary>
        /// The maximum (unbiased) exponent of a <see cref="Double"/> (<c>1023</c>).
        /// </summary>
        public const int DBL_EXP_MAX = 1023;

        /// <summary>
        /// The minimum (unbiased) exponent of a <see cref="Double"/> (<c>-1022</c>).
        /// </summary>
        public const int DBL_EXP_MIN = -1022;

        /// <summary>
        /// Bit-mask used for clearing the exponent bits of a <see cref="Double"/> (<c>0x800fffffffffffff</c>).
        /// </summary>
        public const long DBL_EXP_CLR_MASK = DBL_SGN_MASK | DBL_MANT_MASK;

        /// <summary>
        /// Bit-mask used for extracting the exponent bits of a <see cref="Double"/> (<c>0x7ff0000000000000</c>).
        /// </summary>
        public const long DBL_EXP_MASK = 0x7ff0000000000000L;

        /// <summary>
        /// The number of bits in the mantissa of a <see cref="Double"/>, excludes the implicit leading <c>1</c> bit (<c>52</c>).
        /// </summary>
        public const int DBL_MANT_BITS = 52;

        /// <summary>
        /// Bit-mask used for clearing the mantissa bits of a <see cref="Double"/> (<c>0xfff0000000000000</c>).
        /// </summary>
        public const long DBL_MANT_CLR_MASK = DBL_SGN_MASK | DBL_EXP_MASK;

        /// <summary>
        /// Bit-mask used for extracting the mantissa bits of a <see cref="Double"/> (<c>0x000fffffffffffff</c>).
        /// </summary>
        public const long DBL_MANT_MASK = 0x000fffffffffffffL;

        /// <summary>
        /// Maximum positive, normal value of a <see cref="Double"/> (<c>1.7976931348623157E+308</c>).
        /// </summary>
        public const double DBL_MAX = System.Double.MaxValue;

        /// <summary>
        /// Minimum positive, normal value of a <see cref="Double"/> (<c>2.2250738585072014e-308</c>).
        /// </summary>
        public const double DBL_MIN = 2.2250738585072014e-308D;

        /// <summary>
        /// Maximum positive, subnormal value of a <see cref="Double"/> (<c>2.2250738585072009e-308</c>).
        /// </summary>
        public const double DBL_DENORM_MAX = DBL_MIN - DBL_DENORM_MIN;

        /// <summary>
        /// Minimum positive, subnormal value of a <see cref="Double"/> (<c>4.94065645841247E-324</c>).
        /// </summary>
        public const double DBL_DENORM_MIN = System.Double.Epsilon;

        /// <summary>
        /// Bit-mask used for clearing the sign bit of a <see cref="Double"/> (<c>0x7fffffffffffffff</c>).
        /// </summary>
        public const long DBL_SGN_CLR_MASK = 0x7fffffffffffffffL;

        /// <summary>
        /// Bit-mask used for extracting the sign bit of a <see cref="Double"/> (<c>0x8000000000000000</c>).
        /// </summary>
        public const long DBL_SGN_MASK = -1 - 0x7fffffffffffffffL;

        /// <summary>
        /// The exponent bias of a <see cref="Single"/>, i.e. value to subtract from the stored exponent to get the real exponent (<c>127</c>).
        /// </summary>
        public const int FLT_EXP_BIAS = 127;

        /// <summary>
        /// The number of bits in the exponent of a <see cref="Single"/> (<c>8</c>).
        /// </summary>
        public const int FLT_EXP_BITS = 8;

        /// <summary>
        /// The maximum (unbiased) exponent of a <see cref="Single"/> (<c>127</c>).
        /// </summary>
        public const int FLT_EXP_MAX = 127;

        /// <summary>
        /// The minimum (unbiased) exponent of a <see cref="Single"/> (<c>-126</c>).
        /// </summary>
        public const int FLT_EXP_MIN = -126;

        /// <summary>
        /// Bit-mask used for clearing the exponent bits of a <see cref="Single"/> (<c>0x807fffff</c>).
        /// </summary>
        public const int FLT_EXP_CLR_MASK = FLT_SGN_MASK | FLT_MANT_MASK;

        /// <summary>
        /// Bit-mask used for extracting the exponent bits of a <see cref="Single"/> (<c>0x7f800000</c>).
        /// </summary>
        public const int FLT_EXP_MASK = 0x7f800000;

        /// <summary>
        /// The number of bits in the mantissa of a <see cref="Single"/>, excludes the implicit leading <c>1</c> bit (<c>23</c>).
        /// </summary>
        public const int FLT_MANT_BITS = 23;

        /// <summary>
        /// Bit-mask used for clearing the mantissa bits of a <see cref="Single"/> (<c>0xff800000</c>).
        /// </summary>
        public const int FLT_MANT_CLR_MASK = FLT_SGN_MASK | FLT_EXP_MASK;

        /// <summary>
        /// Bit-mask used for extracting the mantissa bits of a <see cref="Single"/> (<c>0x007fffff</c>).
        /// </summary>
        public const int FLT_MANT_MASK = 0x007fffff;

        /// <summary>
        /// Maximum positive, normal value of a <see cref="Single"/> (<c>3.40282347e+38</c>).
        /// </summary>
        public const float FLT_MAX = System.Single.MaxValue;

        /// <summary>
        /// Minimum positive, normal value of a <see cref="Single"/> (<c>1.17549435e-38</c>).
        /// </summary>
        public const float FLT_MIN = 1.17549435e-38F;

        /// <summary>
        /// Maximum positive, subnormal value of a <see cref="Single"/> (<c>1.17549421e-38</c>).
        /// </summary>
        public const float FLT_DENORM_MAX = FLT_MIN - FLT_DENORM_MIN;

        /// <summary>
        /// Minimum positive, subnormal value of a <see cref="Single"/> (<c>1.401298E-45</c>).
        /// </summary>
        public const float FLT_DENORM_MIN = System.Single.Epsilon;

        /// <summary>
        /// Bit-mask used for clearing the sign bit of a <see cref="Single"/> (<c>0x7fffffff</c>).
        /// </summary>
        public const int FLT_SGN_CLR_MASK = 0x7fffffff;

        /// <summary>
        /// Bit-mask used for extracting the sign bit of a <see cref="Single"/> (<c>0x80000000</c>).
        /// </summary>
        public const int FLT_SGN_MASK = -1 - 0x7fffffff;

        #endregion

        #region "frexp"

        /// <summary>
        /// Decomposes the given floating-point <paramref name="number"/> into a normalized fraction and an integral power of two.
        /// </summary>
        /// <param name="number">A floating-point number.</param>
        /// <param name="exponent">Reference to an <see cref="int"/> value to store the exponent to.</param>
        /// <returns>A <c>fraction</c> in the range <c>[0.5, 1)</c> so that <c><paramref name="number"/> = fraction * 2^<paramref name="exponent"/></c>.</returns>
        /// <remarks>
        /// <para>
        /// Special values are treated as follows.
        /// </para>
        /// <list type="bullet" >
        /// <item>If <paramref name="number"/> is <c>±0</c>, it is returned, and <c>0</c> is returned in <paramref name="exponent"/>.</item>
        /// <item>If <paramref name="number"/> is infinite, it is returned, and an undefined value is returned in <paramref name="exponent"/>.</item>
        /// <item>If <paramref name="number"/> is NaN, it is returned, and an undefined value is returned in <paramref name="exponent"/>.</item>
        /// </list>
        /// <para>
        /// </para>
        /// <para>
        /// The function <see cref="math.frexp(double, ref int)"/>, together with its dual, <see cref="math.ldexp(double, int)"/>,
        /// can be used to manipulate the representation of a floating-point number without direct bit manipulations.
        /// </para>
        /// <para>
        /// The relation of <see cref="math.frexp(double, ref int)"/> to <see cref="math.logb(double)"/> and <see cref="math.scalbn(double, int)"/> is:
        /// </para>
        /// <para>
        /// <c><paramref name="exponent"/> = (<paramref name="number"/> == 0) ? 0 : (int)(1 + <see cref="math.logb(double)">logb</see>(<paramref name="number"/>))</c><br/>
        /// <c>fraction = <see cref="math.scalbn(double, int)">scalbn</see>(<paramref name="number"/>, -<paramref name="exponent"/>)</c>
        /// </para>
        /// <para>
        /// See <a href="http://en.cppreference.com/w/c/numeric/math/frexp">frexp</a> in the C standard documentation.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code language="C#">
        /// Assert.IsTrue(math.frexp(12.8D, ref exponent) = 0.8D);
        /// Assert.IsTrue(exponent = 4);
        /// 
        /// Assert.IsTrue(math.frexp(0.25D, ref exponent) == 0.5D);
        /// Assert.IsTrue(exponent == -1);
        /// 
        /// Assert.IsTrue(math.frexp(System.Math.Pow(2D, 1023), ref exponent) == 0.5D);
        /// Assert.IsTrue(exponent == 1024);
        /// 
        /// Assert.IsTrue(math.frexp(-System.Math.Pow(2D, -1074), ref exponent) == -0.5D);
        /// Assert.IsTrue(exponent == -1073);
        /// </code> 
        /// <code language="VB.NET">
        /// Assert.IsTrue(math.frexp(12.8D, exponent) = 0.8D);
        /// Assert.IsTrue(exponent = 4);
        /// 
        /// Assert.IsTrue(math.frexp(0.25D, exponent) = 0.5D);
        /// Assert.IsTrue(exponent = -1);
        /// 
        /// Assert.IsTrue(math.frexp(System.Math.Pow(2D, 1023), exponent) = 0.5D);
        /// Assert.IsTrue(exponent = 1024);
        /// 
        /// Assert.IsTrue(math.frexp(-System.Math.Pow(2D, -1074), exponent) = -0.5D);
        /// Assert.IsTrue(exponent = -1073);
        /// </code> 
        /// </example>
        public static double frexp(double number, ref int exponent)
        {
            long bits = BitConverter.DoubleToInt64Bits(number);
            int exp = (int)((bits & DBL_EXP_MASK) >> DBL_MANT_BITS);
            exponent = 0;

            if (exp == 0x7ff || number == 0D)
                number += number;
            else
            {
                // Not zero and finite.
                exponent = exp - 1022;
                if (exp == 0)
                {
                    // Subnormal, scale number so that it is in [1, 2).
                    number *= BitConverter.Int64BitsToDouble(0x4350000000000000L); // 2^54
                    bits = BitConverter.DoubleToInt64Bits(number);
                    exp = (int)((bits & DBL_EXP_MASK) >> DBL_MANT_BITS);
                    exponent = exp - 1022 - 54;
                }
                // Set exponent to -1 so that number is in [0.5, 1).
                number = BitConverter.Int64BitsToDouble((bits & DBL_EXP_CLR_MASK) | 0x3fe0000000000000L);
            }

            return number;
        }

        /// <summary>
        /// Decomposes the given floating-point <paramref name="number"/> into a normalized fraction and an integral power of two.
        /// </summary>
        /// <param name="number">A floating-point number.</param>
        /// <param name="exponent">Reference to an <see cref="int"/> value to store the exponent to.</param>
        /// <returns>A <c>fraction</c> in the range <c>[0.5, 1)</c> so that <c><paramref name="number"/> = fraction * 2^<paramref name="exponent"/></c>.</returns>
        /// <remarks>
        /// <para>
        /// Special values are treated as follows.
        /// </para>
        /// <list type="bullet" >
        /// <item>If <paramref name="number"/> is <c>±0</c>, it is returned, and <c>0</c> is returned in <paramref name="exponent"/>.</item>
        /// <item>If <paramref name="number"/> is infinite, it is returned, and an undefined value is returned in <paramref name="exponent"/>.</item>
        /// <item>If <paramref name="number"/> is NaN, it is returned, and an undefined value is returned in <paramref name="exponent"/>.</item>
        /// </list>
        /// <para>
        /// The function <see cref="math.frexp(float, ref int)"/>, together with its dual, <see cref="math.ldexp(float, int)"/>,
        /// can be used to manipulate the representation of a floating-point number without direct bit manipulations.
        /// </para>
        /// <para>
        /// The relation of <see cref="math.frexp(float, ref int)"/> to <see cref="math.logb(float)"/> and <see cref="math.scalbn(float, int)"/> is:
        /// </para>
        /// <para>
        /// <c><paramref name="exponent"/> = (<paramref name="number"/> == 0) ? 0 : (int)(1 + <see cref="math.logb(float)">logb</see>(<paramref name="number"/>))</c><br/>
        /// <c>fraction = <see cref="math.scalbn(float, int)">scalbn</see>(<paramref name="number"/>, -<paramref name="exponent"/>)</c>
        /// </para>
        /// <para>
        /// See <a href="http://en.cppreference.com/w/c/numeric/math/frexp">frexp</a> in the C standard documentation.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code language="C#">
        /// Assert.IsTrue(math.frexp(12.8F, ref exponent) = 0.8F);
        /// Assert.IsTrue(exponent = 4);
        /// 
        /// Assert.IsTrue(math.frexp(0.25F, ref exponent) == 0.5F);
        /// Assert.IsTrue(exponent == -1);
        /// 
        /// Assert.IsTrue(math.frexp(System.Math.Pow(2F, 127F), ref exponent) == 0.5F);
        /// Assert.IsTrue(exponent == 128);
        /// 
        /// Assert.IsTrue(math.frexp(-System.Math.Pow(2F, -149F), ref exponent) == -0.5F);
        /// Assert.IsTrue(exponent == -148);
        /// </code> 
        /// <code language="VB.NET">
        /// Assert.IsTrue(math.frexp(12.8F, exponent) = 0.8F);
        /// Assert.IsTrue(exponent = 4);
        /// 
        /// Assert.IsTrue(math.frexp(0.25F, exponent) = 0.5F);
        /// Assert.IsTrue(exponent = -1);
        /// 
        /// Assert.IsTrue(math.frexp(System.Math.Pow(2F, 127F), exponent) = 0.5F);
        /// Assert.IsTrue(exponent = 128);
        /// 
        /// Assert.IsTrue(math.frexp(-System.Math.Pow(2F, -149F), exponent) = -0.5F);
        /// Assert.IsTrue(exponent = -148);
        /// </code> 
        /// </example>
        public static float frexp(float number, ref int exponent)
        {
            int bits = SingleToInt32Bits(number);
            int exp = (int)((bits & FLT_EXP_MASK) >> FLT_MANT_BITS);
            exponent = 0;

            if (exp == 0xff || number == 0F)
                number += number;
            else
            {
                // Not zero and finite.
                exponent = exp - 126;
                if (exp == 0)
                {
                    // Subnormal, scale number so that it is in [1, 2).
                    number *= Int32BitsToSingle(0x4c000000); // 2^25
                    bits = SingleToInt32Bits(number);
                    exp = (int)((bits & FLT_EXP_MASK) >> FLT_MANT_BITS);
                    exponent = exp - 126 - 25;
                }
                // Set exponent to -1 so that number is in [0.5, 1).
                number = Int32BitsToSingle((bits & FLT_EXP_CLR_MASK) | 0x3f000000);
            }

            return number;
        }

        #endregion

        #region "Miscellaneous functions."

        /// <summary>
        /// Converts the specified single-precision floating point number to a 32-bit signed integer.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A 32-bit signed integer whose value is equivalent to <paramref name="value"/>.</returns>
        public static int SingleToInt32Bits(float value)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
        }

        /// <summary>
        /// Converts the specified 32-bit signed integer to a single-precision floating point number.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A double-precision floating point number whose value is equivalent to <paramref name="value"/>.</returns>
        public static float Int32BitsToSingle(int value)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(value), 0);
        }

        #endregion

        #endregion
    }
#pragma warning restore IDE1006
}
