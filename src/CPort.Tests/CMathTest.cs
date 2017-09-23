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
    }
}
