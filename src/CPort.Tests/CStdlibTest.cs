using static CPort.C;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CPort.Tests
{
    public class CStdlibTest
    {
        [Fact]
        public void Cstrtod()
        {
            Pointer<char> end = NULL;
            Pointer<char> str = "".GetPointer();

            Assert.Equal(0, strtod(NULL, out end));
            Assert.True(NULL == end);

            Assert.Equal(0, strtod(str, out end));
            Assert.True(str == end);

            str = "  ".GetPointer();
            Assert.Equal(0, strtod(str, out end));
            Assert.Equal(2, end.Index);

            str = "  123.45e+2".GetPointer();
            Assert.Equal(123.45e+2, strtod(str, out end));
            Assert.Equal(11, end.Index);

            str = "  +123.45e2".GetPointer();
            Assert.Equal(123.45e+2, strtod(str, out end));
            Assert.Equal(11, end.Index);

            str = "  -123.45".GetPointer();
            Assert.Equal(-123.45, strtod(str, out end));
            Assert.Equal(9, end.Index);

            str = "  -123.45Test".GetPointer();
            Assert.Equal(-123.45, strtod(str, out end));
            Assert.Equal(9, end.Index);

            str = "  .45".GetPointer();
            Assert.Equal(.45, strtod(str, out end));
            Assert.Equal(5, end.Index);

            str = "  .".GetPointer();
            Assert.Equal(0, strtod(str, out end));
            Assert.Equal(2, end.Index);

            str = "  0D".GetPointer();
            Assert.Equal(0, strtod(str, out end));
            Assert.Equal(3, end.Index);

            str = "  0xD".GetPointer();
            Assert.Equal(0xD, strtod(str, out end));
            Assert.Equal(5, end.Index);

            str = "  -0xD".GetPointer();
            Assert.Equal(-0xD, strtod(str, out end));
            Assert.Equal(6, end.Index);

            str = "  0xZ".GetPointer();
            Assert.Equal(0, strtod(str, out end));
            Assert.Equal(3, end.Index);

            str = "  0.123".GetPointer();
            Assert.Equal(0.123, strtod(str, out end));
            Assert.Equal(7, end.Index);

            str = "  0123".GetPointer();
            Assert.Equal(123, strtod(str, out end));
            Assert.Equal(6, end.Index);

            str = "  01234567890123456789".GetPointer();
            Assert.Equal(1234567890123456789, strtod(str, out end));
            Assert.Equal(22, end.Index);

            str = "  Infinity".GetPointer();
            Assert.Equal(double.PositiveInfinity, strtod(str, out end));
            Assert.Equal(10, end.Index);

            str = "  +Infinity".GetPointer();
            Assert.Equal(double.PositiveInfinity, strtod(str, out end));
            Assert.Equal(11, end.Index);

            str = "  -Infinity".GetPointer();
            Assert.Equal(double.NegativeInfinity, strtod(str, out end));
            Assert.Equal(11, end.Index);

            str = "  Information".GetPointer();
            Assert.Equal(double.PositiveInfinity, strtod(str, out end));
            Assert.Equal(5, end.Index);

            str = "  +Information".GetPointer();
            Assert.Equal(double.PositiveInfinity, strtod(str, out end));
            Assert.Equal(6, end.Index);

            str = "  -Information".GetPointer();
            Assert.Equal(double.NegativeInfinity, strtod(str, out end));
            Assert.Equal(6, end.Index);

            str = "  Internet".GetPointer();
            Assert.Equal(0, strtod(str, out end));
            Assert.Equal(2, end.Index);

            str = "  NaN".GetPointer();
            Assert.Equal(double.NaN, strtod(str, out end));
            Assert.Equal(5, end.Index);

            str = "  NoN".GetPointer();
            Assert.Equal(0, strtod(str, out end));
            Assert.Equal(2, end.Index);

            str = "  Test".GetPointer();
            Assert.Equal(0, strtod(str, out end));
            Assert.Equal(2, end.Index);
        }

        [Fact]
        public void Catof()
        {
            Pointer<char> str = "".GetPointer();

            Assert.Equal(0, atof(NULL));

            Assert.Equal(0, atof(str));

            str = "  ".GetPointer();
            Assert.Equal(0, atof(str));

            str = "  123.45e+2".GetPointer();
            Assert.Equal(123.45e+2, atof(str));

            str = "  +123.45e2".GetPointer();
            Assert.Equal(123.45e+2, atof(str));

            str = "  -123.45".GetPointer();
            Assert.Equal(-123.45, atof(str));

            str = "  -123.45Test".GetPointer();
            Assert.Equal(-123.45, atof(str));

            str = "  .45".GetPointer();
            Assert.Equal(.45, atof(str));

            str = "  .".GetPointer();
            Assert.Equal(0, atof(str));

            str = "  0D".GetPointer();
            Assert.Equal(0, atof(str));

            str = "  0xD".GetPointer();
            Assert.Equal(0xD, atof(str));

            str = "  -0xD".GetPointer();
            Assert.Equal(-0xD, atof(str));

            str = "  0xZ".GetPointer();
            Assert.Equal(0, atof(str));

            str = "  0.123".GetPointer();
            Assert.Equal(0.123, atof(str));

            str = "  0123".GetPointer();
            Assert.Equal(123, atof(str));

            str = "  01234567890123456789".GetPointer();
            Assert.Equal(1234567890123456789, atof(str));

            str = "  Infinity".GetPointer();
            Assert.Equal(double.PositiveInfinity, atof(str));

            str = "  +Infinity".GetPointer();
            Assert.Equal(double.PositiveInfinity, atof(str));

            str = "  -Infinity".GetPointer();
            Assert.Equal(double.NegativeInfinity, atof(str));

            str = "  Information".GetPointer();
            Assert.Equal(double.PositiveInfinity, atof(str));

            str = "  +Information".GetPointer();
            Assert.Equal(double.PositiveInfinity, atof(str));

            str = "  -Information".GetPointer();
            Assert.Equal(double.NegativeInfinity, atof(str));

            str = "  Internet".GetPointer();
            Assert.Equal(0, atof(str));

            str = "  NaN".GetPointer();
            Assert.Equal(double.NaN, atof(str));

            str = "  NoN".GetPointer();
            Assert.Equal(0, atof(str));

            str = "  Test".GetPointer();
            Assert.Equal(0, atof(str));
        }

        [Fact]
        public void Cstrtol()
        {
            Pointer<char> end = NULL;
            Pointer<char> str = "".GetPointer();

            Assert.Equal(0, strtol(NULL, out end, 0));
            Assert.True(NULL == end);

            Assert.Equal(0, strtol(str, out end, 0));
            Assert.True(str == end);

            str = "  ".GetPointer();
            Assert.Equal(0, strtol(str, out end, 0));
            Assert.Equal(2, end.Index);

            str = "  123.45e+2".GetPointer();
            Assert.Equal(123, strtol(str, out end, 0));
            Assert.Equal(5, end.Index);

            str = "  +123.45e2".GetPointer();
            Assert.Equal(123, strtol(str, out end, 0));
            Assert.Equal(6, end.Index);

            str = "  -123.45".GetPointer();
            Assert.Equal(-123, strtol(str, out end, 0));
            Assert.Equal(6, end.Index);

            str = "  0123".GetPointer();
            Assert.Equal(83, strtol(str, out end, 0));
            Assert.Equal(6, end.Index);

            str = "  0xD".GetPointer();
            Assert.Equal(0xD, strtol(str, out end, 0));
            Assert.Equal(5, end.Index);

            str = "  0xDz".GetPointer();
            Assert.Equal(0xD, strtol(str, out end, 16));
            Assert.Equal(5, end.Index);

            str = "  Dz".GetPointer();
            Assert.Equal(0xD, strtol(str, out end, 16));
            Assert.Equal(3, end.Index);

            str = "  0x".GetPointer();
            Assert.Equal(0, strtol(str, out end, 16));
            Assert.Equal(2, end.Index);

            str = "  148".GetPointer();
            Assert.Equal(12, strtol(str, out end, 8));
            Assert.Equal(4, end.Index);

            str = "  Infinity".GetPointer();
            Assert.Equal(0, strtol(str, out end, 8));
            Assert.Equal(2, end.Index);

            str = "  Infinity".GetPointer();
            Assert.Equal(1461559270678, strtol(str, out end, 36));
            Assert.Equal(10, end.Index);

            str = "  -INFINITY".GetPointer();
            Assert.Equal(-1461559270678, strtol(str, out end, 36));
            Assert.Equal(11, end.Index);
        }

        [Fact]
        public void Cstrtoul()
        {
            Pointer<char> end = NULL;
            Pointer<char> str = "".GetPointer();

            Assert.Equal(0u, strtoul(NULL, out end, 0));
            Assert.True(NULL == end);

            Assert.Equal(0u, strtoul(str, out end, 0));
            Assert.True(str == end);

            str = "  ".GetPointer();
            Assert.Equal(0u, strtoul(str, out end, 0));
            Assert.Equal(2, end.Index);

            str = "  123.45e+2".GetPointer();
            Assert.Equal(123u, strtoul(str, out end, 0));
            Assert.Equal(5, end.Index);

            str = "  +123.45e2".GetPointer();
            Assert.Equal(0u, strtoul(str, out end, 0));
            Assert.Equal(2, end.Index);

            str = "  -123.45".GetPointer();
            Assert.Equal(0u, strtoul(str, out end, 0));
            Assert.Equal(2, end.Index);

            str = "  0123".GetPointer();
            Assert.Equal(83u, strtoul(str, out end, 0));
            Assert.Equal(6, end.Index);

            str = "  0xD".GetPointer();
            Assert.Equal(0xDu, strtoul(str, out end, 0));
            Assert.Equal(5, end.Index);

            str = "  0xDz".GetPointer();
            Assert.Equal(0xDu, strtoul(str, out end, 16));
            Assert.Equal(5, end.Index);

            str = "  Dz".GetPointer();
            Assert.Equal(0xDu, strtoul(str, out end, 16));
            Assert.Equal(3, end.Index);

            str = "  0x".GetPointer();
            Assert.Equal(0u, strtoul(str, out end, 16));
            Assert.Equal(2, end.Index);

            str = "  148".GetPointer();
            Assert.Equal(12u, strtoul(str, out end, 8));
            Assert.Equal(4, end.Index);

            str = "  Infinity".GetPointer();
            Assert.Equal(0u, strtoul(str, out end, 8));
            Assert.Equal(2, end.Index);

            str = "  Infinity".GetPointer();
            Assert.Equal(1461559270678u, strtoul(str, out end, 36));
            Assert.Equal(10, end.Index);
        }

    }
}
