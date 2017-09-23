using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CPort.Tests.Extensions
{
    public class PointerExtensionsTest
    {

        [Fact]
        public void GetStringFromPointerOfChar()
        {
            var pointer = new Pointer<char>(new char[] { 'T', 'e', 's', 't', '\0', 'T', 'e', 's', 't', '\0' });
            Assert.Equal("Test", pointer.GetString());

            Assert.Equal("st", (pointer + 2).GetString());
            Assert.Equal("", (pointer + 4).GetString());
            Assert.Equal("est", (pointer + 6).GetString());
            Assert.Equal("t", (pointer + 8).GetString());
            Assert.Equal("", (pointer + 10).GetString());

            pointer = (char[])null;
            Assert.Equal(null, pointer.GetString());
            Assert.Equal(null, (pointer + 2).GetString());

        }

    }
}
