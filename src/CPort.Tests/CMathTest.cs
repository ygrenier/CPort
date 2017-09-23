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
    }
}
