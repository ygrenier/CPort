using System;
using System.IO;
using System.Text;
using Xunit;

namespace CPort.Tests
{
    public class CScanfTest
    {

        [Fact]
        public void TestScanfChar()
        {
            char r1 = ' ';
            Pointer<char> pr1 = new Pointer<char>(new char[] { ' ', ' ', ' ' });
            PChar pr2 = new PChar(new char[] { ' ', ' ', ' ' });

            Assert.Equal(-1, C.sscanf("", "%c", ref r1));
            Assert.Equal(' ', r1);
            Assert.Equal(-1, C.sscanf("", "%c", pr1));
            Assert.Equal(new char[] { ' ', ' ', ' ' }, pr1.Source);
            Assert.Equal(-1, C.sscanf("", "%c", pr2));
            Assert.Equal(new char[] { ' ', ' ', ' ' }, pr2.Source);

            Assert.Equal(0, C.sscanf("1", "%2c", ref r1));
            Assert.Equal(' ', r1);
            Assert.Equal(0, C.sscanf("1", "%2c", pr1));
            Assert.Equal(new char[] { ' ', ' ', ' ' }, pr1.Source);
            Assert.Equal(0, C.sscanf("1", "%2c", pr2));
            Assert.Equal(new char[] { ' ', ' ', ' ' }, pr2.Source);

            Assert.Equal(1, C.sscanf("123", "%c", ref r1));
            Assert.Equal('1', r1);
            Assert.Equal(1, C.sscanf("123", "%c", pr1));
            Assert.Equal(new char[] { '1', ' ', ' ' }, pr1.Source);
            Assert.Equal(1, C.sscanf("123", "%c", pr2));
            Assert.Equal(new char[] { '1', ' ', ' ' }, pr2.Source);

            char[] r2 = null;
            Assert.Equal(1, C.sscanf("abc", "%2c", ref r2));
            Assert.Equal(new char[] { 'a', 'b' }, r2);
            Assert.Equal(1, C.sscanf("abc", "%2c", pr1));
            Assert.Equal(new char[] { 'a', 'b', ' ' }, pr1.Source);
            Assert.Equal(1, C.sscanf("abc", "%2c", pr2));
            Assert.Equal(new char[] { 'a', 'b', ' ' }, pr2.Source);
        }

        [Fact]
        public void TestScanfByte()
        {
            sbyte r1 = 0; Pointer<sbyte> pr1 = new Pointer<sbyte>(3);

            Assert.Equal(1, C.sscanf("123", "%hhd", ref r1));
            Assert.Equal(123, r1);
            Assert.Equal(1, C.sscanf("123", "%hhd", pr1));
            Assert.Equal(new sbyte[] { 123, 0, 0 }, pr1.Source);

            byte r2 = 0; Pointer<byte> pr2 = new Pointer<byte>(3);

            Assert.Equal(1, C.sscanf("123", "%hhu", ref r2));
            Assert.Equal(123, r2);
            Assert.Equal(1, C.sscanf("123", "%hhu", pr2));
            Assert.Equal(new byte[] { 123, 0, 0 }, pr2.Source);
        }

        [Fact]
        public void TestScanfInt16()
        {
            Int16 r1 = 0; Pointer<Int16> pr1 = new Pointer<short>(3);

            Assert.Equal(1, C.sscanf("123", "%hd", ref r1));
            Assert.Equal(123, r1);
            Assert.Equal(1, C.sscanf("123", "%hd", pr1));
            Assert.Equal(new Int16[] { 123, 0, 0 }, pr1.Source);

            UInt16 r2 = 0; Pointer<UInt16> pr2 = new Pointer<ushort>(3);

            Assert.Equal(1, C.sscanf("123", "%hu", ref r2));
            Assert.Equal(123, r2);
            Assert.Equal(1, C.sscanf("123", "%hu", pr2));
            Assert.Equal(new UInt16[] { 123, 0, 0 }, pr2.Source);
        }

        [Fact]
        public void TestScanfInt32()
        {
            int r1 = 0; Pointer<Int32> pr1 = new Pointer<int>(3);

            Assert.Equal(-1, C.sscanf("", "%d", ref r1));
            Assert.Equal(0, r1);
            Assert.Equal(-1, C.sscanf("", "%d", pr1));
            Assert.Equal(new int[] { 0, 0, 0 }, pr1.Source);

            Assert.Equal(0, C.sscanf("[ ", "[%d", ref r1));
            Assert.Equal(0, r1);
            Assert.Equal(0, C.sscanf("[ ", "[%d", pr1));
            Assert.Equal(new int[] { 0, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("  123 ", "%d", ref r1));
            Assert.Equal(123, r1);
            Assert.Equal(1, C.sscanf("  123 ", "%d", pr1));
            Assert.Equal(new int[] { 123, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("  321 ", "%d %f", ref r1));
            Assert.Equal(321, r1);
            Assert.Equal(1, C.sscanf("  321 ", "%d %f", pr1));
            Assert.Equal(new int[] { 321, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("+123 ", "%d", ref r1));
            Assert.Equal(123, r1);
            Assert.Equal(1, C.sscanf("+123 ", "%d", pr1 + 2));
            Assert.Equal(new int[] { 321, 0, 123 }, pr1.Source);

            Assert.Equal(1, C.sscanf("-123 ", "%d", ref r1));
            Assert.Equal(-123, r1);
            Assert.Equal(1, C.sscanf("-123 ", "%d", pr1));
            Assert.Equal(new int[] { -123, 0, 123 }, pr1.Source);

            Assert.Equal(1, C.sscanf("040", "%d", ref r1));
            Assert.Equal(40, r1);
            Assert.Equal(1, C.sscanf("040", "%d", pr1));
            Assert.Equal(new int[] { 40, 0, 123 }, pr1.Source);

            Assert.Equal(1, C.sscanf("0x40", "%d", ref r1));
            Assert.Equal(0, r1);
            Assert.Equal(1, C.sscanf("0x40", "%d", pr1));
            Assert.Equal(new int[] { 0, 0, 123 }, pr1.Source);

            Assert.Equal(1, C.sscanf("123 ", "%2d", ref r1));
            Assert.Equal(12, r1);
            Assert.Equal(1, C.sscanf("123 ", "%2d", pr1));
            Assert.Equal(new int[] { 12, 0, 123 }, pr1.Source);

            Assert.Equal(1, C.sscanf("123", "%5d", ref r1));
            Assert.Equal(123, r1);
            Assert.Equal(1, C.sscanf("123", "%5d", pr1));
            Assert.Equal(new int[] { 123, 0, 123 }, pr1.Source);
        }

        [Fact]
        public void TestScanfUInt32()
        {
            uint r1 = 0; Pointer<UInt32> pr1 = new Pointer<uint>(3);

            Assert.Equal(-1, C.sscanf("", "%u", ref r1));
            Assert.Equal((uint)0, r1);
            Assert.Equal(-1, C.sscanf("", "%u", pr1));
            Assert.Equal(new uint[] { 0, 0, 0 }, pr1.Source);

            Assert.Equal(0, C.sscanf(" ", "%u", ref r1));
            Assert.Equal((uint)0, r1);
            Assert.Equal(0, C.sscanf(" ", "%u", pr1));
            Assert.Equal(new uint[] { 0, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("  123 ", "%u", ref r1));
            Assert.Equal((uint)123, r1);
            Assert.Equal(1, C.sscanf("  123 ", "%u", pr1));
            Assert.Equal(new uint[] { 123, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("+123 ", "%u", ref r1));
            Assert.Equal((uint)123, r1);
            Assert.Equal(1, C.sscanf("+123 ", "%u", pr1));
            Assert.Equal(new uint[] { 123, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("040", "%u", ref r1));
            Assert.Equal((uint)40, r1);
            Assert.Equal(1, C.sscanf("040", "%u", pr1));
            Assert.Equal(new uint[] { 40, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("0x40", "%u", ref r1));
            Assert.Equal((uint)0, r1);
            Assert.Equal(1, C.sscanf("0x40", "%u", pr1));
            Assert.Equal(new uint[] { 0, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("123 ", "%2u", ref r1));
            Assert.Equal((uint)12, r1);
            Assert.Equal(1, C.sscanf("123 ", "%2u", pr1));
            Assert.Equal(new uint[] { 12, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("123", "%4u", ref r1));
            Assert.Equal((uint)123, r1);
            Assert.Equal(1, C.sscanf("123", "%4u", pr1));
            Assert.Equal(new uint[] { 123, 0, 0 }, pr1.Source);

        }

        [Fact]
        public void TestScanfInt64()
        {
            Int64 r1 = 0; Pointer<Int64> pr1 = new Pointer<long>(3);

            Assert.Equal(1, C.sscanf("123", "%ld", ref r1));
            Assert.Equal(123, r1);
            Assert.Equal(1, C.sscanf("123", "%ld", pr1));
            Assert.Equal(new Int64[] { 123, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("123", "%lld", ref r1));
            Assert.Equal(123, r1);
            Assert.Equal(1, C.sscanf("123", "%lld", pr1));
            Assert.Equal(new Int64[] { 123, 0, 0 }, pr1.Source);
        }

        [Fact]
        public void TestScanfUInt64()
        {
            UInt64 r1 = 0; Pointer<UInt64> pr1 = new Pointer<ulong>(3);

            Assert.Equal(1, C.sscanf("123", "%lu", ref r1));
            Assert.Equal((UInt64)123, r1);
            Assert.Equal(1, C.sscanf("123", "%lu", pr1));
            Assert.Equal(new UInt64[] { 123, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("123", "%llu", ref r1));
            Assert.Equal((UInt64)123, r1);
            Assert.Equal(1, C.sscanf("123", "%llu", pr1));
            Assert.Equal(new UInt64[] { 123, 0, 0 }, pr1.Source);
        }

        [Fact]
        public void TestScanfFloat()
        {
            Single r1 = 0; Double r2 = 0;
            Pointer<Single> pr1 = new Pointer<float>(3);
            Pointer<double> pr2 = new Pointer<double>(3);

            Assert.Equal(1, C.sscanf("123", "%f", ref r1));
            Assert.Equal(123.0, r1);
            Assert.Equal(1, C.sscanf("123.456", "%f", ref r1));
            Assert.Equal(123.456f, r1);
            Assert.Equal(1, C.sscanf("123", "%f", pr1));
            Assert.Equal(new float[] { 123.0f, 0, 0 }, pr1.Source);
            Assert.Equal(1, C.sscanf("123.456", "%f", pr1));
            Assert.Equal(new float[] { 123.456f, 0, 0 }, pr1.Source);

            Assert.Equal(2, C.sscanf("123.456-9876.543", "%f-%lf", ref r1, ref r2));
            Assert.Equal(123.456f, r1);
            Assert.Equal(9876.543, r2);
            Assert.Equal(2, C.sscanf("123.456-9876.543", "%f-%lf", pr1, pr2));
            Assert.Equal(new float[] { 123.456f, 0, 0 }, pr1.Source);
            Assert.Equal(new double[] { 9876.543, 0, 0 }, pr2.Source);

            r1 = 0; pr1 = new Pointer<float>(3);
            Assert.Equal(-1, C.sscanf("", "%f", ref r1));
            Assert.Equal(0f, r1);
            Assert.Equal(-1, C.sscanf("", "%f", pr1));
            Assert.Equal(new float[] { 0, 0, 0 }, pr1.Source);

            Assert.Equal(0, C.sscanf("[ ", "[%f", ref r1));
            Assert.Equal(0f, r1);
            Assert.Equal(0, C.sscanf("[ ", "[%f", pr1));
            Assert.Equal(new float[] { 0, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("  123 ", "%f", ref r1));
            Assert.Equal(123f, r1);
            Assert.Equal(1, C.sscanf("  123 ", "%f", pr1));
            Assert.Equal(new float[] { 123, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("+123 ", "%f", ref r1));
            Assert.Equal(123f, r1);
            Assert.Equal(1, C.sscanf("+123 ", "%f", pr1));
            Assert.Equal(new float[] { 123, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("-123 ", "%f", ref r1));
            Assert.Equal(-123f, r1);
            Assert.Equal(1, C.sscanf("-123 ", "%f", pr1));
            Assert.Equal(new float[] { -123, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("040", "%f", ref r1));
            Assert.Equal(40f, r1);
            Assert.Equal(1, C.sscanf("040", "%f", pr1));
            Assert.Equal(new float[] { 40, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("0x40", "%f", ref r1));
            Assert.Equal(0f, r1);
            Assert.Equal(1, C.sscanf("0x40", "%f", pr1));
            Assert.Equal(new float[] { 0, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("123 ", "%2f", ref r1));
            Assert.Equal(12f, r1);
            Assert.Equal(1, C.sscanf("123 ", "%2f", pr1));
            Assert.Equal(new float[] { 12, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("123", "%5f", ref r1));
            Assert.Equal(123f, r1);
            Assert.Equal(1, C.sscanf("123", "%5f", pr1));
            Assert.Equal(new float[] { 123, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("12.34.56", "%f", ref r1));
            Assert.Equal(12.34f, r1);
            Assert.Equal(1, C.sscanf("12.34.56", "%f", pr1));
            Assert.Equal(new float[] { 12.34f, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("12.34e2", "%f", ref r1));
            Assert.Equal(1234f, r1);
            Assert.Equal(1, C.sscanf("12.34e2", "%f", pr1));
            Assert.Equal(new float[] { 1234, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("12.34e+2", "%f", ref r1));
            Assert.Equal(1234f, r1);
            Assert.Equal(1, C.sscanf("12.34e+2", "%f", pr1));
            Assert.Equal(new float[] { 1234, 0, 0 }, pr1.Source);

            Assert.Equal(1, C.sscanf("12.34e-2", "%f", ref r1));
            Assert.Equal(0.1234f, r1);
            Assert.Equal(1, C.sscanf("12.34e-2", "%f", pr1));
            Assert.Equal(new float[] { 0.1234f, 0, 0 }, pr1.Source);
        }

        [Fact]
        public void TestScanfOctal()
        {
            uint r1 = 0;

            Assert.Equal(0, C.sscanf(" ", "%o", ref r1));
            Assert.Equal((uint)0, r1);

            Assert.Equal(1, C.sscanf("123", "%o", ref r1));
            Assert.Equal((uint)83, r1);

            Assert.Equal(1, C.sscanf("545", "%2o", ref r1));
            Assert.Equal((uint)44, r1);

            Assert.Equal(1, C.sscanf("545", "%4o", ref r1));
            Assert.Equal((uint)357, r1);

        }

        [Fact]
        public void TestScanfHexa()
        {
            uint r1 = 0;

            Assert.Equal(0, C.sscanf(" ", "%x", ref r1));
            Assert.Equal((uint)0, r1);

            Assert.Equal(1, C.sscanf("123", "%x", ref r1));
            Assert.Equal((uint)291, r1);

            Assert.Equal(1, C.sscanf("0x123", "%x", ref r1));
            Assert.Equal((uint)291, r1);

            Assert.Equal(1, C.sscanf("123", "%2x", ref r1));
            Assert.Equal((uint)18, r1);

            Assert.Equal(1, C.sscanf("123", "%4x", ref r1));
            Assert.Equal((uint)291, r1);

        }

        [Fact]
        public void TestScanfString()
        {
            String r1 = null;
            Pointer<char> pr1 = new Pointer<char>(new char[] { '-', '-', '-', '-', '-' });
            PChar pr2 = new PChar(new char[] { '-', '-', '-', '-', '-' });

            Assert.Equal(0, C.sscanf(" ", "%s", ref r1));
            Assert.Equal(null, r1);
            Assert.Equal(0, C.sscanf(" ", "%s", pr1));
            Assert.Equal(new char[] { '-', '-', '-', '-', '-' }, pr1.Source);
            Assert.Equal(0, C.sscanf(" ", "%s", pr2));
            Assert.Equal(new char[] { '-', '-', '-', '-', '-' }, pr2.Source);
            Assert.Equal("-----", pr2.GetString());

            Assert.Equal(1, C.sscanf("abc", "%s", ref r1));
            Assert.Equal("abc", r1);
            Assert.Equal(1, C.sscanf("abc", "%s", pr1));
            Assert.Equal(new char[] { 'a', 'b', 'c', '\0', '-' }, pr1.Source);
            Assert.Equal(1, C.sscanf("abc", "%s", pr2));
            Assert.Equal(new char[] { 'a', 'b', 'c', '\0', '-' }, pr2.Source);
            Assert.Equal("abc", pr2.GetString());

            Assert.Equal(1, C.sscanf("abc", "%2s", ref r1));
            Assert.Equal("ab", r1);
            Assert.Equal(1, C.sscanf("abc", "%2s", pr1));
            Assert.Equal(new char[] { 'a', 'b', '\0', '\0', '-' }, pr1.Source);
            Assert.Equal(1, C.sscanf("abc", "%2s", pr2));
            Assert.Equal(new char[] { 'a', 'b', '\0', '\0', '-' }, pr2.Source);
            Assert.Equal("ab", pr2.GetString());

            Assert.Equal(1, C.sscanf("abc", "%4s", ref r1));
            Assert.Equal("abc", r1);
            Assert.Equal(1, C.sscanf("abc", "%4s", pr1));
            Assert.Equal(new char[] { 'a', 'b', 'c', '\0', '-' }, pr1.Source);
            Assert.Equal(1, C.sscanf("abc", "%4s", pr2));
            Assert.Equal(new char[] { 'a', 'b', 'c', '\0', '-' }, pr2.Source);
            Assert.Equal("abc", pr2.GetString());

        }

        [Fact]
        public void TestScanfChars()
        {
            int r1 = 0;

            Assert.Equal(0, C.sscanf("123", "[%d]", ref r1));
            Assert.Equal(0, r1);

            Assert.Equal(1, C.sscanf("[123]", "[%d]", ref r1));
            Assert.Equal(123, r1);

        }

        [Fact]
        public void TestScanfWidth()
        {
            int r1 = 0;

            Assert.Equal(1, C.sscanf("[123]", "[%2d]", ref r1));
            Assert.Equal(12, r1);

        }

        [Fact]
        public void TestScanfScanSet()
        {
            Int32 r1 = 0, r2 = 0;
            String r3 = null, r4 = null;

            Assert.Equal(4, C.sscanf("Copyright 2009-2011 CompanyName (Multi-Word message)", "Copyright %d-%d %s (%[^)]", ref r1, ref r2, ref r3, ref r4));
            Assert.Equal(2009, r1);
            Assert.Equal(2011, r2);
            Assert.Equal("CompanyName", r3);
            Assert.Equal("Multi-Word message", r4);

            r3 = null;

            Assert.Equal(0, C.sscanf("( ", "(%[itluM]", ref r3));
            Assert.Equal(null, r3);

            Assert.Equal(1, C.sscanf("(Multi-Word message)", "(%[itluM]", ref r3));
            Assert.Equal("Multi", r3);

            Assert.Equal(1, C.sscanf("(Multi-Word message)", "(%2[itluM]", ref r3));
            Assert.Equal("Mu", r3);

            Assert.Equal(1, C.sscanf("(Multi-Word message)", "(%8[itluM]", ref r3));
            Assert.Equal("Multi", r3);

            r3 = null;
            Assert.Equal(1, C.sscanf("[Multi-Word] message", "%[^]]", ref r3));
            Assert.Equal("[Multi-Word", r3);
        }

        [Fact]
        public void TestScanfScanSetInvalidCloseBracket()
        {
            String r3 = null;
            Assert.Throws<Exception>(() => C.sscanf("([Multi-Word] message)", "(%[]", ref r3));
        }

        [Fact]
        public void TestScanfPercent()
        {
            int r1 = 0;

            Assert.Equal(1, C.sscanf("%123%", "%%%d%%", ref r1));
            Assert.Equal(123, r1);

        }

        [Fact]
        public void TestScanfNoStored()
        {
            int r1 = 0, r2 = 0;

            Assert.Equal(1, C.sscanf("123-456", "%*d-%d", ref r1, ref r2));
            Assert.Equal(456, r1);
            Assert.Equal(0, r2);

        }

        [Fact]
        public void TestScanfScanSetUnknownTypeSpecifier()
        {
            String r3 = null;
            Assert.Throws<Exception>(() => C.sscanf("1234", "%z", ref r3));
        }

        [Fact]
        public void TestSScanf()
        {
            Int32 r1 = 0, r2 = 0;
            String r3 = null, r4 = null;
            Int32 r5 = 0, r6 = 0;

            String test = "Copyright 2009-2011 CompanyName (Multi-Word message) - 123 - 987";

            r1 = r2 = 0; r3 = r4 = null;
            Assert.Equal(1, C.sscanf(test, "Copyright %d", ref r1));
            Assert.Equal(2009, r1);
            Assert.Equal(0, r2);
            Assert.Equal(null, r3);
            Assert.Equal(null, r4);

            r1 = r2 = 0; r3 = r4 = null;
            Assert.Equal(2, C.sscanf(test, "Copyright %d-%d", ref r1, ref r2));
            Assert.Equal(2009, r1);
            Assert.Equal(2011, r2);
            Assert.Equal(null, r3);
            Assert.Equal(null, r4);

            r1 = r2 = 0; r3 = r4 = null;
            Assert.Equal(3, C.sscanf(test, "Copyright %d-%d %s", ref r1, ref r2, ref r3));
            Assert.Equal(2009, r1);
            Assert.Equal(2011, r2);
            Assert.Equal("CompanyName", r3);
            Assert.Equal(null, r4);

            r1 = r2 = 0; r3 = r4 = null;
            Assert.Equal(4, C.sscanf(test, "Copyright %d-%d %s (%[^)]", ref r1, ref r2, ref r3, ref r4));
            Assert.Equal(2009, r1);
            Assert.Equal(2011, r2);
            Assert.Equal("CompanyName", r3);
            Assert.Equal("Multi-Word message", r4);

            r1 = r2 = 0; r3 = r4 = null;
            Assert.Equal(5, C.sscanf(test, "Copyright %d-%d %s (%[^)] - %d", ref r1, ref r2, ref r3, ref r4, ref r5));
            Assert.Equal(2009, r1);
            Assert.Equal(2011, r2);
            Assert.Equal("CompanyName", r3);
            Assert.Equal("Multi-Word message", r4);
            Assert.Equal(123, r5);

            r1 = r2 = r5 = 0; r3 = r4 = null;
            Assert.Equal(6, C.sscanf(test, "Copyright %d-%d %s (%[^)] - %d - %d", ref r1, ref r2, ref r3, ref r4, ref r5, ref r6));
            Assert.Equal(2009, r1);
            Assert.Equal(2011, r2);
            Assert.Equal("CompanyName", r3);
            Assert.Equal("Multi-Word message", r4);
            Assert.Equal(123, r5);
            Assert.Equal(987, r6);

        }

        [Fact]
        public void TestSScanfWithPointers()
        {
            Pointer<Int32> r1 = new Pointer<int>(new int[] { 0 }), r2 = new Pointer<int>(new int[] { 0 });
            PChar r3 = C.NULL; Pointer<char> r4 = C.NULL;
            Pointer<Int32> r5 = new Pointer<int>(new int[] { 0 }), r6 = new Pointer<int>(new int[] { 0 });

            String test = "Copyright 2009-2011 CompanyName (Multi-Word message) - 123 - 987";

            r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
            Assert.Equal(1, C.sscanf(test, "Copyright %d", r1, r2, r3, r4, r5, r6));
            Assert.Equal(2009, r1);
            Assert.Equal(0, r2);
            Assert.Equal(string.Empty, r3.GetString());
            Assert.Equal(string.Empty, ((PChar)r4).GetString());
            Assert.Equal(0, r5);
            Assert.Equal(0, r6);

            r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
            Assert.Equal(2, C.sscanf(test, "Copyright %d-%d", r1, r2, r3, r4, r5, r6));
            Assert.Equal(2009, r1);
            Assert.Equal(2011, r2);
            Assert.Equal(string.Empty, r3.GetString());
            Assert.Equal(string.Empty, ((PChar)r4).GetString());
            Assert.Equal(0, r5);
            Assert.Equal(0, r6);

            r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
            Assert.Equal(3, C.sscanf(test, "Copyright %d-%d %s", r1, r2, r3, r4, r5, r6));
            Assert.Equal(2009, r1);
            Assert.Equal(2011, r2);
            Assert.Equal("CompanyName", r3.GetString());
            Assert.Equal(string.Empty, ((PChar)r4).GetString());
            Assert.Equal(0, r5);
            Assert.Equal(0, r6);

            r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
            Assert.Equal(4, C.sscanf(test, "Copyright %d-%d %s (%[^)]", r1, r2, r3, r4, r5, r6));
            Assert.Equal(2009, r1);
            Assert.Equal(2011, r2);
            Assert.Equal("CompanyName", r3.GetString());
            Assert.Equal("Multi-Word message", ((PChar)r4).GetString());
            Assert.Equal(0, r5);
            Assert.Equal(0, r6);

            r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
            Assert.Equal(5, C.sscanf(test, "Copyright %d-%d %s (%[^)] - %d", r1, r2, r3, r4, r5, r6));
            Assert.Equal(2009, r1);
            Assert.Equal(2011, r2);
            Assert.Equal("CompanyName", r3.GetString());
            Assert.Equal("Multi-Word message", ((PChar)r4).GetString());
            Assert.Equal(123, r5);
            Assert.Equal(0, r6);

            r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
            Assert.Equal(6, C.sscanf(test, "Copyright %d-%d %s (%[^)] - %d - %d", r1, r2, r3, r4, r5, r6));
            Assert.Equal(2009, r1);
            Assert.Equal(2011, r2);
            Assert.Equal("CompanyName", r3.GetString());
            Assert.Equal("Multi-Word message", ((PChar)r4).GetString());
            Assert.Equal(123, r5);
            Assert.Equal(987, r6);

            r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
            Assert.Equal(6, C.sscanf(test, "Copyright %d-%d %s (%[^)] - %d - %d", r6, r5, r2, r1, r4, r3));
            Assert.Equal(0, r1);
            Assert.Equal(0, r2);
            Assert.Equal(string.Empty, r3.GetString());
            Assert.Equal(string.Empty, ((PChar)r4).GetString());
            Assert.Equal(2011, r5);
            Assert.Equal(2009, r6);

            Assert.Equal(6, C.sscanf(test, "Copyright %d-%d %s (%[^)] - %d - %d", null));
            Assert.Equal(6, C.sscanf(test, "Copyright %d-%d %s (%[^)] - %d - %d",
                new Pointer<sbyte>(1), new Pointer<byte>(1), new Pointer<ushort>(1), 
                new Pointer<short>(1), new Pointer<ulong>(1), new Pointer<long>(1)));
            Assert.Equal(6, C.sscanf(test, "Copyright %d-%d %s (%[^)] - %d - %d",
                new Pointer<uint>(1), new Pointer<float>(1), new Pointer<double>(1)));
        }

        [Fact]
        public void TestFScanf()
        {
            Int32 r1 = 0, r2 = 0;
            String r3 = null, r4 = null;
            Int32 r5 = 0, r6 = 0;

            String test = "Copyright 2009-2011 CompanyName (Multi-Word message) - 123 - 987";
            byte[] buffer = Encoding.ASCII.GetBytes(test);

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                r1 = r2 = 0; r3 = r4 = null;
                Assert.Equal(1, C.fscanf(file, "Copyright %d", ref r1));
                Assert.Equal(2009, r1);
                Assert.Equal(0, r2);
                Assert.Equal(null, r3);
                Assert.Equal(null, r4);
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                r1 = r2 = 0; r3 = r4 = null;
                Assert.Equal(2, C.fscanf(file, "Copyright %d-%d", ref r1, ref r2));
                Assert.Equal(2009, r1);
                Assert.Equal(2011, r2);
                Assert.Equal(null, r3);
                Assert.Equal(null, r4);
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                r1 = r2 = 0; r3 = r4 = null;
                Assert.Equal(3, C.fscanf(file, "Copyright %d-%d %s", ref r1, ref r2, ref r3));
                Assert.Equal(2009, r1);
                Assert.Equal(2011, r2);
                Assert.Equal("CompanyName", r3);
                Assert.Equal(null, r4);
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                r1 = r2 = 0; r3 = r4 = null;
                Assert.Equal(4, C.fscanf(file, "Copyright %d-%d %s (%[^)]", ref r1, ref r2, ref r3, ref r4));
                Assert.Equal(2009, r1);
                Assert.Equal(2011, r2);
                Assert.Equal("CompanyName", r3);
                Assert.Equal("Multi-Word message", r4);
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                r1 = r2 = 0; r3 = r4 = null;
                Assert.Equal(5, C.fscanf(file, "Copyright %d-%d %s (%[^)] - %d", ref r1, ref r2, ref r3, ref r4, ref r5));
                Assert.Equal(2009, r1);
                Assert.Equal(2011, r2);
                Assert.Equal("CompanyName", r3);
                Assert.Equal("Multi-Word message", r4);
                Assert.Equal(123, r5);
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                r1 = r2 = r5 = 0; r3 = r4 = null;
                Assert.Equal(6, C.fscanf(file, "Copyright %d-%d %s (%[^)] - %d - %d", ref r1, ref r2, ref r3, ref r4, ref r5, ref r6));
                Assert.Equal(2009, r1);
                Assert.Equal(2011, r2);
                Assert.Equal("CompanyName", r3);
                Assert.Equal("Multi-Word message", r4);
                Assert.Equal(123, r5);
                Assert.Equal(987, r6);
            }

            using (var file = new FILE(new MemoryStream(Encoding.ASCII.GetBytes("abc")), CFileMode.Read, Encoding.ASCII))
            {
                r1 = r2 = r5 = r6 = 0; r3 = r4 = null;
                Assert.Equal(0, C.fscanf(file, "Copyright %d-%d %s (%[^)] - %d - %d", ref r1, ref r2, ref r3, ref r4, ref r5, ref r6));
                Assert.Equal(0, r1);
                Assert.Equal(0, r2);
                Assert.Equal(null, r3);
                Assert.Equal(null, r4);
                Assert.Equal(0, r5);
                Assert.Equal(0, r6);
            }

            using (var file = new FILE(new MemoryStream(), CFileMode.Read, Encoding.ASCII))
            {
                r1 = r2 = r5 = r6 = 0; r3 = r4 = null;
                Assert.Equal(-1, C.fscanf(file, "Copyright %d-%d %s (%[^)] - %d - %d", ref r1, ref r2, ref r3, ref r4, ref r5, ref r6));
                Assert.Equal(0, r1);
                Assert.Equal(0, r2);
                Assert.Equal(null, r3);
                Assert.Equal(null, r4);
                Assert.Equal(0, r5);
                Assert.Equal(0, r6);
            }
        }

        [Fact]
        public void TestFScanfWithPointers()
        {
            Pointer<Int32> r1 = new Pointer<int>(new int[] { 0 }), r2 = new Pointer<int>(new int[] { 0 });
            PChar r3 = C.NULL; Pointer<char> r4 = C.NULL;
            Pointer<Int32> r5 = new Pointer<int>(new int[] { 0 }), r6 = new Pointer<int>(new int[] { 0 });

            String test = "Copyright 2009-2011 CompanyName (Multi-Word message) - 123 - 987";
            byte[] buffer = Encoding.ASCII.GetBytes(test);

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
                Assert.Equal(1, C.fscanf(file, "Copyright %d", r1, r2, r3, r4, r5, r6));
                Assert.Equal(2009, r1);
                Assert.Equal(0, r2);
                Assert.Equal(string.Empty, r3.GetString());
                Assert.Equal(string.Empty, ((PChar)r4).GetString());
                Assert.Equal(0, r5);
                Assert.Equal(0, r6);
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
                Assert.Equal(2, C.fscanf(file, "Copyright %d-%d", r1, r2, r3, r4, r5, r6));
                Assert.Equal(2009, r1);
                Assert.Equal(2011, r2);
                Assert.Equal(string.Empty, r3.GetString());
                Assert.Equal(string.Empty, ((PChar)r4).GetString());
                Assert.Equal(0, r5);
                Assert.Equal(0, r6);
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
                Assert.Equal(3, C.fscanf(file, "Copyright %d-%d %s", r1, r2, r3, r4, r5, r6));
                Assert.Equal(2009, r1);
                Assert.Equal(2011, r2);
                Assert.Equal("CompanyName", r3.GetString());
                Assert.Equal(string.Empty, ((PChar)r4).GetString());
                Assert.Equal(0, r5);
                Assert.Equal(0, r6);
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
                Assert.Equal(4, C.fscanf(file, "Copyright %d-%d %s (%[^)]", r1, r2, r3, r4, r5, r6));
                Assert.Equal(2009, r1);
                Assert.Equal(2011, r2);
                Assert.Equal("CompanyName", r3.GetString());
                Assert.Equal("Multi-Word message", ((PChar)r4).GetString());
                Assert.Equal(0, r5);
                Assert.Equal(0, r6);
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
                Assert.Equal(5, C.fscanf(file, "Copyright %d-%d %s (%[^)] - %d", r1, r2, r3, r4, r5, r6));
                Assert.Equal(2009, r1);
                Assert.Equal(2011, r2);
                Assert.Equal("CompanyName", r3.GetString());
                Assert.Equal("Multi-Word message", ((PChar)r4).GetString());
                Assert.Equal(123, r5);
                Assert.Equal(0, r6);
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
                Assert.Equal(6, C.fscanf(file, "Copyright %d-%d %s (%[^)] - %d - %d", r1, r2, r3, r4, r5, r6));
                Assert.Equal(2009, r1);
                Assert.Equal(2011, r2);
                Assert.Equal("CompanyName", r3.GetString());
                Assert.Equal("Multi-Word message", ((PChar)r4).GetString());
                Assert.Equal(123, r5);
                Assert.Equal(987, r6);
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
                Assert.Equal(6, C.fscanf(file, "Copyright %d-%d %s (%[^)] - %d - %d", r6, r5, r2, r1, r4, r3));
                Assert.Equal(0, r1);
                Assert.Equal(0, r2);
                Assert.Equal(string.Empty, r3.GetString());
                Assert.Equal(string.Empty, ((PChar)r4).GetString());
                Assert.Equal(2011, r5);
                Assert.Equal(2009, r6);
            }

            using (var file = new FILE(new MemoryStream(), CFileMode.Read, Encoding.ASCII))
            {
                r1.Value = r2.Value = r5.Value = r6.Value = 0; r3 = new PChar(255); r4 = new Pointer<char>(255);
                Assert.Equal(-1, C.fscanf(file, "Copyright %d-%d %s (%[^)] - %d - %d", r6, r5, r2, r1, r4, r3));
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                Assert.Equal(6, C.fscanf(file, "Copyright %d-%d %s (%[^)] - %d - %d", null));
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                Assert.Equal(6, C.fscanf(file, "Copyright %d-%d %s (%[^)] - %d - %d",
                new Pointer<sbyte>(1), new Pointer<byte>(1), new Pointer<ushort>(1),
                new Pointer<short>(1), new Pointer<ulong>(1), new Pointer<long>(1)));
            }

            using (var file = new FILE(new MemoryStream(buffer), CFileMode.Read, Encoding.ASCII))
            {
                Assert.Equal(6, C.fscanf(file, "Copyright %d-%d %s (%[^)] - %d - %d",
                new Pointer<uint>(1), new Pointer<float>(1), new Pointer<double>(1)));
            }
        }

    }
}
