using static CPort.C;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CPort.Tests
{
    public class CMathTest
    {
        [Fact]
        public void Csin()
        {
            Assert.Equal(Math.Sin(0), sin(0));
            Assert.Equal(Math.Sin(0.5), sin(0.5));
            Assert.Equal(Math.Sin(1), sin(1));
        }

        [Fact]
        public void Ccos()
        {
            Assert.Equal(Math.Cos(0), cos(0));
            Assert.Equal(Math.Cos(0.5), cos(0.5));
            Assert.Equal(Math.Cos(1), cos(1));
        }

        [Fact]
        public void Ctan()
        {
            Assert.Equal(Math.Tan(0), tan(0));
            Assert.Equal(Math.Tan(0.5), tan(0.5));
            Assert.Equal(Math.Tan(1), tan(1));
        }

        [Fact]
        public void Casin()
        {
            Assert.Equal(Math.Asin(0), asin(0));
            Assert.Equal(Math.Asin(0.5), asin(0.5));
            Assert.Equal(Math.Asin(1), asin(1));
        }

        [Fact]
        public void Cacos()
        {
            Assert.Equal(Math.Acos(0), acos(0));
            Assert.Equal(Math.Acos(0.5), acos(0.5));
            Assert.Equal(Math.Acos(1), acos(1));
        }

        [Fact]
        public void Catan()
        {
            Assert.Equal(Math.Atan(0), atan(0));
            Assert.Equal(Math.Atan(0.5), atan(0.5));
            Assert.Equal(Math.Atan(1), atan(1));
        }

        [Fact]
        public void Catan2()
        {
            Assert.Equal(Math.Atan2(0.5, 0), atan2(0.5, 0));
            Assert.Equal(Math.Atan2(0.5, 0.5), atan2(0.5, 0.5));
            Assert.Equal(Math.Atan2(0.5, 1), atan2(0.5, 1));
        }

        [Fact]
        public void Csinh()
        {
            Assert.Equal(Math.Sinh(0), sinh(0));
            Assert.Equal(Math.Sinh(0.5), sinh(0.5));
            Assert.Equal(Math.Sinh(1), sinh(1));
        }

        [Fact]
        public void Ccosh()
        {
            Assert.Equal(Math.Cosh(0), cosh(0));
            Assert.Equal(Math.Cosh(0.5), cosh(0.5));
            Assert.Equal(Math.Cosh(1), cosh(1));
        }

        [Fact]
        public void Ctanh()
        {
            Assert.Equal(Math.Tanh(0), tanh(0));
            Assert.Equal(Math.Tanh(0.5), tanh(0.5));
            Assert.Equal(Math.Tanh(1), tanh(1));
        }

        [Fact]
        public void Clog()
        {
            Assert.Equal(Math.Log(0), log(0));
            Assert.Equal(Math.Log(0.5), log(0.5));
            Assert.Equal(Math.Log(1), log(1));
        }

        [Fact]
        public void Clog10()
        {
            Assert.Equal(Math.Log10(0), log10(0));
            Assert.Equal(Math.Log10(0.5), log10(0.5));
            Assert.Equal(Math.Log10(1), log10(1));
        }

        [Fact]
        public void Cpow()
        {
            Assert.Equal(Math.Pow(12, 0), pow(12, 0));
            Assert.Equal(Math.Pow(12, 0.5), pow(12, 0.5));
            Assert.Equal(Math.Pow(12, 1), pow(12, 1));
        }

        [Fact]
        public void Csqrt()
        {
            Assert.Equal(Math.Sqrt(0), sqrt(0));
            Assert.Equal(Math.Sqrt(0.5), sqrt(0.5));
            Assert.Equal(Math.Sqrt(1), sqrt(1));
        }

        [Fact]
        public void Cceil()
        {
            Assert.Equal(Math.Ceiling(0d), ceil(0));
            Assert.Equal(Math.Ceiling(0.5), ceil(0.5));
            Assert.Equal(Math.Ceiling(1d), ceil(1));
        }

        [Fact]
        public void Cfloor()
        {
            Assert.Equal(Math.Floor(0d), floor(0));
            Assert.Equal(Math.Floor(0.5), floor(0.5));
            Assert.Equal(Math.Floor(1d), floor(1));
        }

        [Fact]
        public void Cfabs()
        {
            Assert.Equal(Math.Abs(0d), fabs(0));
            Assert.Equal(Math.Abs(0.5), fabs(0.5));
            Assert.Equal(Math.Abs(1d), fabs(1));
        }

        [Fact]
        public void Cldexp()
        {
            Assert.Equal(0.0d * Math.Pow(2, 5), ldexp(0, 5));
            Assert.Equal(0.5d * Math.Pow(2, 5), ldexp(0.5, 5));
            Assert.Equal(1.0d * Math.Pow(2, 5), ldexp(1, 5));
        }

        [Fact]
        public void Cfrexp_double()
        {
            int exponent = 0;
            Assert.Equal(0.0D, frexp(0.0D, ref exponent));
            Assert.Equal(0, exponent);

            Assert.True(frexp(12.8D, ref exponent) == 0.8D);
            Assert.True(exponent == 4);

            Assert.True(frexp(0.25D, ref exponent) == 0.5D);
            Assert.True(exponent == -1);

            Assert.True(frexp(Math.Pow(2D, 1023), ref exponent) == 0.5D);
            Assert.True(exponent == 1024);

            Assert.True(frexp(-Math.Pow(2D, -1074), ref exponent) == -0.5D);
            Assert.True(exponent == -1073);

        }

        [Fact]
        public void Cfrexp_float()
        {
            int exponent = 0;
            Assert.Equal(0.0F, frexp(0.0F, ref exponent));
            Assert.Equal(0, exponent);

            Assert.True(frexp(12.8F, ref exponent) == 0.8F);
            Assert.True(exponent == 4);

            Assert.True(frexp(0.25F, ref exponent) == 0.5F);
            Assert.True(exponent == -1);

            Assert.True(frexp((float)Math.Pow(2F, 127F), ref exponent) == 0.5F);
            Assert.True(exponent == 128);

            Assert.True(frexp((float)-Math.Pow(2F, -149F), ref exponent) == -0.5F);
            Assert.True(exponent == -148);
        }

        [Fact]
        public void Cmodf()
        {
            double ip = 0;
            Assert.Equal(0.1416, modf(3.1416, ref ip), 4);
            Assert.Equal(3.0, ip);

            Assert.Equal(-0.1416, modf(-3.1416, ref ip), 4);
            Assert.Equal(-3.0, ip);

            Assert.Equal(0.9876, modf(4.9876, ref ip), 4);
            Assert.Equal(4.0, ip);

            Assert.Equal(-0.9876, modf(-4.9876, ref ip), 4);
            Assert.Equal(-4.0, ip);
        }

    }
}
