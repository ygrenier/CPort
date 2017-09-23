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

    }
}
