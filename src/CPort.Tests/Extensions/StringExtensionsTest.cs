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

        [Fact]
        public void GetPointer()
        {
            string s = "Test";
            var p = s.GetPointer();
            Assert.False(p.IsNull);
            Assert.Equal(5, p.Source.Count);
            Assert.Equal('T', p[0]);
            Assert.Equal('e', p[1]);
            Assert.Equal('s', p[2]);
            Assert.Equal('t', p[3]);
            Assert.Equal('\0', p[4]);

            s = "";
            p = s.GetPointer();
            Assert.False(p.IsNull);
            Assert.Equal(1, p.Source.Count);
            Assert.Equal('\0', p[0]);

            s = null;
            p = s.GetPointer();
            Assert.True(p.IsNull);
            Assert.Null(p.Source);
        }

    }
}
