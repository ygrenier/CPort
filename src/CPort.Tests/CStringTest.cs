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

        [Fact]
        public void Cstrcat()
        {
            var source = "Test".GetPointer();
            var dest = "012\04567890123456789".GetPointer();

            var actual = strcat(dest, source);
            Assert.Equal(new char[] { '0', '1', '2', 'T', 'e', 's', 't', '\0', '8' }, actual.Take(9));
            Assert.Equal("012Test\0890123456789\0", new string(actual.ToArray()));
        }

        [Fact]
        public void Cstrncat()
        {
            var source = "Test".GetPointer();
            var dest = "012\04567890123456789".GetPointer();

            var actual = strncat(dest + 1, source, 3);
            Assert.Equal("012Tes\07890123456789\0", new string(dest.ToArray()));
            Assert.Equal("12Tes\07890123456789\0", new string(actual.ToArray()));

            actual = strncat(dest, source, 7);
            Assert.Equal("012TesTest\0\0\0\0456789\0", new string(actual.ToArray()));
        }

        [Fact]
        public void Cstrcmp()
        {
            var s1 = "a".GetPointer();
            var s2 = "a".GetPointer();
            Assert.True(strcmp(s1, s2) == 0);

            s1 = "a".GetPointer();
            s2 = "A".GetPointer();
            Assert.True(strcmp(s1, s2) > 0);

            s1 = "A".GetPointer();
            s2 = "a".GetPointer();
            Assert.True(strcmp(s1, s2) < 0);

            s1 = "aBc".GetPointer();
            s2 = "abc".GetPointer();
            Assert.True(strcmp(s1, s2) < 0);

            s1 = "ab".GetPointer();
            s2 = "abc".GetPointer();
            Assert.True(strcmp(s1, s2) < 0);

            s1 = "abc".GetPointer();
            s2 = "ab".GetPointer();
            Assert.True(strcmp(s1, s2) > 0);
        }

        [Fact]
        public void Cstrncmp()
        {
            var s1 = "a".GetPointer();
            var s2 = "a".GetPointer();
            Assert.True(strncmp(s1, s2, 3) == 0);

            s1 = "a".GetPointer();
            s2 = "A".GetPointer();
            Assert.True(strncmp(s1, s2, 3) > 0);

            s1 = "A".GetPointer();
            s2 = "a".GetPointer();
            Assert.True(strncmp(s1, s2, 3) < 0);

            s1 = "aBc".GetPointer();
            s2 = "abc".GetPointer();
            Assert.True(strncmp(s1, s2, 3) < 0);

            s1 = "ab".GetPointer();
            s2 = "abc".GetPointer();
            Assert.True(strncmp(s1, s2, 3) < 0);

            s1 = "abc".GetPointer();
            s2 = "ab".GetPointer();
            Assert.True(strncmp(s1, s2, 3) > 0);

            s1 = "aBc".GetPointer();
            s2 = "abc".GetPointer();
            Assert.True(strncmp(s1, s2, 2) < 0);

            s1 = "ab".GetPointer();
            s2 = "abc".GetPointer();
            Assert.True(strncmp(s1, s2, 2) == 0);

            s1 = "abc".GetPointer();
            s2 = "ab".GetPointer();
            Assert.True(strncmp(s1, s2, 2) == 0);
        }

    }
}
