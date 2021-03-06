﻿using static CPort.C;
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

        [Fact]
        public void Cstrchr()
        {
            var cs = "abCdeCgh".GetPointer();

            var actual = strchr(cs, 'C');
            Assert.False(actual.IsNull);
            Assert.Equal(2, actual.Index);

            actual = strchr(cs, 'c');
            Assert.True(actual.IsNull);
        }

        [Fact]
        public void Cstrrchr()
        {
            var cs = "abCdeCgh".GetPointer();

            var actual = strrchr(cs, 'C');
            Assert.False(actual.IsNull);
            Assert.Equal(5, actual.Index);

            actual = strrchr(cs, 'c');
            Assert.True(actual.IsNull);
        }

        [Fact]
        public void Cstrspn()
        {
            var str = "129th".GetPointer();
            var keys = "1234567890".GetPointer();

            Assert.Equal(3, strspn(str, keys));

            str = "12".GetPointer();
            Assert.Equal(2, strspn(str, keys));

            str = "ab12".GetPointer();
            Assert.Equal(0, strspn(str, keys));
        }

        [Fact]
        public void Cstrcspn()
        {
            var str = "fcba73".GetPointer();
            var keys = "1234567890".GetPointer();

            Assert.Equal(4, strcspn(str, keys));

            str = "fc".GetPointer();
            Assert.Equal(2, strcspn(str, keys));

            str = "12ab".GetPointer();
            Assert.Equal(0, strcspn(str, keys));
        }

        [Fact]
        public void Cstrpbrk()
        {
            var str = "This is a sample string".GetPointer();
            var keys = "aeiou".GetPointer();

            var actual = strpbrk(str, keys);
            Assert.False(actual.IsNull);
            Assert.Equal(2, actual.Index);

            actual = strpbrk(actual + 1, keys);
            Assert.False(actual.IsNull);
            Assert.Equal(5, actual.Index);

            actual = strpbrk(actual + 1, keys);
            Assert.False(actual.IsNull);
            Assert.Equal(8, actual.Index);

            actual = strpbrk(actual + 1, keys);
            Assert.False(actual.IsNull);
            Assert.Equal(11, actual.Index);

            actual = strpbrk(actual + 1, keys);
            Assert.False(actual.IsNull);
            Assert.Equal(15, actual.Index);

            actual = strpbrk(actual + 1, keys);
            Assert.False(actual.IsNull);
            Assert.Equal(20, actual.Index);

            actual = strpbrk(actual + 1, keys);
            Assert.True(actual.IsNull);
        }

        [Fact]
        public void Cstrstr()
        {
            var str = "This is a sample string".GetPointer();
            var search = "sample".GetPointer();

            // In the string
            var actual = strstr(str, search);
            Assert.False(actual.IsNull);
            Assert.Equal(10, actual.Index);

            // Not in the string, with common start
            search = "samPle".GetPointer();
            actual = strstr(str, search);
            Assert.True(actual.IsNull);

            // Not in the string
            search = "Sample".GetPointer();
            actual = strstr(str, search);
            Assert.True(actual.IsNull);

            // At the end of the string 
            search = "string".GetPointer();
            actual = strstr(str, search);
            Assert.False(actual.IsNull);
            Assert.Equal(17, actual.Index);

            // Not in the string but start at the end of the string
            search = "ringer".GetPointer();
            actual = strstr(str, search);
            Assert.True(actual.IsNull);
        }

        [Fact]
        public void Cstrlen()
        {
            var str = "Test".GetPointer();
            Assert.Equal(4, strlen(str));

            str = "Other test\0with AZT in the buffer.".GetPointer();
            Assert.Equal(10, strlen(str));

            str = new Pointer<char>();
            Assert.Equal(0, strlen(str));
        }

        [Fact]
        public void Cstrtok()
        {
            var str = "- This, a sample string.".GetPointer();
            var delimiters = " -.,".GetPointer();

            Assert.True(strtok(null, delimiters).IsNull);

            var actual = strtok(str, delimiters);
            Assert.False(actual.IsNull);
            Assert.Equal(2, actual.Index);
            Assert.Equal("This", actual.GetString());

            actual = strtok(null, delimiters);
            Assert.False(actual.IsNull);
            Assert.Equal(8, actual.Index);
            Assert.Equal("a", actual.GetString());

            actual = strtok(null, delimiters);
            Assert.False(actual.IsNull);
            Assert.Equal(10, actual.Index);
            Assert.Equal("sample", actual.GetString());

            actual = strtok(null, delimiters);
            Assert.False(actual.IsNull);
            Assert.Equal(17, actual.Index);
            Assert.Equal("string", actual.GetString());

            actual = strtok(null, delimiters);
            Assert.True(actual.IsNull);
            actual = strtok(null, delimiters);
            Assert.True(actual.IsNull);

            // Restart
            actual = strtok(str, delimiters);
            Assert.False(actual.IsNull);
            Assert.Equal(2, actual.Index);
            Assert.Equal("This", actual.GetString());

            actual = strtok(null, delimiters);
            Assert.False(actual.IsNull);
            Assert.Equal(8, actual.Index);
            Assert.Equal("a", actual.GetString());

            actual = strtok(str, delimiters);
            Assert.False(actual.IsNull);
            Assert.Equal(2, actual.Index);
            Assert.Equal("This", actual.GetString());
        }

    }
}
