using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CPort.Tests.Extensions
{
    public class StringExtensionsTest
    {
        [Fact]
        public void ContainsChar()
        {
            string line = "AbC";

            Assert.True(line.Contains('A'));
            Assert.True(line.Contains('b'));
            Assert.False(line.Contains('c'));
            Assert.True(line.Contains('C'));

            line = null;

            Assert.False(line.Contains('A'));
            Assert.False(line.Contains('b'));
            Assert.False(line.Contains('c'));
            Assert.False(line.Contains('C'));
        }
    }
}
