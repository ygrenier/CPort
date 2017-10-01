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
            PChar source = "Test";
            PChar dest = "01234567890123456789";

            var actual = strcpy(dest, source);
            Assert.Equal(new char[] { 'T', 'e', 's', 't', '\0', '5' }, actual.Take(6));
            Assert.Equal("Test\0567890123456789\0", new string(actual.ToArray()));
        }

        [Fact]
        public void Cstrncpy()
        {
            PChar source = "Test";
            PChar dest = "01234567890123456789";

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
            PChar source = "Test";
            PChar dest = "012\04567890123456789";

            var actual = strcat(dest, source);
            Assert.Equal(new char[] { '0', '1', '2', 'T', 'e', 's', 't', '\0', '8' }, actual.Take(9));
            Assert.Equal("012Test\0890123456789\0", new string(actual.ToArray()));
        }

        [Fact]
        public void Cstrncat()
        {
            PChar source = "Test";
            PChar dest = "012\04567890123456789";

            var actual = strncat(dest + 1, source, 3);
            Assert.Equal("012Tes\07890123456789\0", new string(dest.ToArray()));
            Assert.Equal("12Tes\07890123456789\0", new string(actual.ToArray()));

            actual = strncat(dest, source, 7);
            Assert.Equal("012TesTest\0\0\0\0456789\0", new string(actual.ToArray()));
        }

        [Fact]
        public void Cstrcmp()
        {
            PChar s1 = "a";
            PChar s2 = "a";
            Assert.True(strcmp(s1, s2) == 0);

            s1 = "a";
            s2 = "A";
            Assert.True(strcmp(s1, s2) > 0);

            s1 = "A";
            s2 = "a";
            Assert.True(strcmp(s1, s2) < 0);

            s1 = "aBc";
            s2 = "abc";
            Assert.True(strcmp(s1, s2) < 0);

            s1 = "ab";
            s2 = "abc";
            Assert.True(strcmp(s1, s2) < 0);

            s1 = "abc";
            s2 = "ab";
            Assert.True(strcmp(s1, s2) > 0);
        }

        [Fact]
        public void Cstrncmp()
        {
            PChar s1 = "a";
            PChar s2 = "a";
            Assert.True(strncmp(s1, s2, 3) == 0);

            s1 = "a";
            s2 = "A";
            Assert.True(strncmp(s1, s2, 3) > 0);

            s1 = "A";
            s2 = "a";
            Assert.True(strncmp(s1, s2, 3) < 0);

            s1 = "aBc";
            s2 = "abc";
            Assert.True(strncmp(s1, s2, 3) < 0);

            s1 = "ab";
            s2 = "abc";
            Assert.True(strncmp(s1, s2, 3) < 0);

            s1 = "abc";
            s2 = "ab";
            Assert.True(strncmp(s1, s2, 3) > 0);

            s1 = "aBc";
            s2 = "abc";
            Assert.True(strncmp(s1, s2, 2) < 0);

            s1 = "ab";
            s2 = "abc";
            Assert.True(strncmp(s1, s2, 2) == 0);

            s1 = "abc";
            s2 = "ab";
            Assert.True(strncmp(s1, s2, 2) == 0);
        }

        [Fact]
        public void Cstrchr()
        {
            PChar cs = "abCdeCgh";

            var actual = strchr(cs, 'C');
            Assert.False(actual.IsNull);
            Assert.Equal(2, actual.Index);

            actual = strchr(cs, 'c');
            Assert.True(actual.IsNull);
        }

        [Fact]
        public void Cstrrchr()
        {
            PChar cs = "abCdeCgh";

            var actual = strrchr(cs, 'C');
            Assert.False(actual.IsNull);
            Assert.Equal(5, actual.Index);

            actual = strrchr(cs, 'c');
            Assert.True(actual.IsNull);
        }

        [Fact]
        public void Cstrspn()
        {
            PChar str = "129th";
            PChar keys = "1234567890";

            Assert.Equal(3, strspn(str, keys));

            str = "12";
            Assert.Equal(2, strspn(str, keys));

            str = "ab12";
            Assert.Equal(0, strspn(str, keys));
        }

        [Fact]
        public void Cstrcspn()
        {
            PChar str = "fcba73";
            PChar keys = "1234567890";

            Assert.Equal(4, strcspn(str, keys));

            str = "fc";
            Assert.Equal(2, strcspn(str, keys));

            str = "12ab";
            Assert.Equal(0, strcspn(str, keys));
        }

        [Fact]
        public void Cstrpbrk()
        {
            PChar str = "This is a sample string";
            PChar keys = "aeiou";

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
            PChar str = "This is a sample string";
            PChar search = "sample";

            // In the string
            var actual = strstr(str, search);
            Assert.False(actual.IsNull);
            Assert.Equal(10, actual.Index);

            // Not in the string, with common start
            search = "samPle";
            actual = strstr(str, search);
            Assert.True(actual.IsNull);

            // Not in the string
            search = "Sample";
            actual = strstr(str, search);
            Assert.True(actual.IsNull);

            // At the end of the string 
            search = "string";
            actual = strstr(str, search);
            Assert.False(actual.IsNull);
            Assert.Equal(17, actual.Index);

            // Not in the string but start at the end of the string
            search = "ringer";
            actual = strstr(str, search);
            Assert.True(actual.IsNull);
        }

        [Fact]
        public void Cstrlen()
        {
            PChar str = "Test";
            Assert.Equal(4, strlen(str));

            str = "Other test\0with AZT in the buffer.";
            Assert.Equal(10, strlen(str));

            str = new PChar();
            Assert.Equal(0, strlen(str));
        }

        [Fact]
        public void Cstrtok()
        {
            PChar str = "- This, a sample string.";
            PChar delimiters = " -.,";

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
