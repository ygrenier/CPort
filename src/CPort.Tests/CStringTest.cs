using static CPort.C;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace CPort.Tests
{
    public class CStringTest
    {
        [Fact]
        public void Cstrcpy()
        {
            var source = "Test".GetPointer();
            var dest = "01234567890123456789".GetPointer();

            var actual = strcpy(dest, source);
            Assert.Equal(new char[] { 'T', 'e', 's', 't', '\0', '5' }, actual.Take(6));
            Assert.Equal("Test\0567890123456789\0", new string(actual.ToArray()));
        }

        [Fact]
        public void Cstrncpy()
        {
            var source = "Test".GetPointer();
            var dest = "01234567890123456789".GetPointer();

            var actual = strncpy(dest + 1, source, 3);
            Assert.Equal(new char[] { '0', 'T', 'e', 's', '4', '5' }, dest.Take(6));
            Assert.Equal(new char[] { 'T', 'e', 's', '4', '5', '6' }, actual.Take(6));
            Assert.Equal("0Tes4567890123456789\0", new string(dest.ToArray()));
            Assert.Equal("Tes4567890123456789\0", new string(actual.ToArray()));

            actual = strncpy(dest, source, 7);
            Assert.Equal(new char[] { 'T', 'e', 's', 't', '\0', '\0', '\0', '7' }, actual.Take(8));
            Assert.Equal("Test\0\0\07890123456789\0", new string(actual.ToArray()));

        }
    }
}
