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
    }
}
