using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace CPort.Tests
{
    public class FileTest
    {

        [Fact]
        public void Create()
        {
            var s = new MemoryStream();
            var m = CFileMode.Read;

            var f = new FILE(s, m, Encoding.Default);
            Assert.Same(s, f.Source);
            Assert.Same(Encoding.Default, f.Encoding);
            Assert.Equal(m, f.Mode);
            Assert.Equal(0, f.Position);

            Assert.Throws<ArgumentNullException>(() => new FILE(s, m, null));
            Assert.Throws<ArgumentNullException>(() => new FILE(null, m, Encoding.Default));

            m = CFileMode.Read | CFileMode.Binary;
            f = new FILE(s, m, Encoding.Default);
            Assert.Same(s, f.Source);
            Assert.Same(Encoding.Default, f.Encoding);
            Assert.Equal(m, f.Mode);
            Assert.Equal(0, f.Position);

            f = new FILE(s, m, null);
            Assert.Same(s, f.Source);
            Assert.Null(f.Encoding);
            Assert.Equal(m, f.Mode);
            Assert.Equal(0, f.Position);

            m = CFileMode.Write;
            f = new FILE(s, m, Encoding.Default);
            Assert.Same(s, f.Source);
            Assert.Same(Encoding.Default, f.Encoding);
            Assert.Equal(m, f.Mode);
            Assert.Equal(0, f.Position);

            Assert.Throws<ArgumentNullException>(() => new FILE(s, m, null));

            m = CFileMode.Write | CFileMode.Binary;
            f = new FILE(s, m, Encoding.Default);
            Assert.Same(s, f.Source);
            Assert.Same(Encoding.Default, f.Encoding);
            Assert.Equal(m, f.Mode);
            Assert.Equal(0, f.Position);

            f = new FILE(s, m, null);
            Assert.Same(s, f.Source);
            Assert.Null(f.Encoding);
            Assert.Equal(m, f.Mode);
            Assert.Equal(0, f.Position);

            Assert.Throws<ArgumentNullException>(() => new FILE(null, m, Encoding.Default));
        }

        [Fact]
        public void Dispose()
        {
            var s = new MemoryStream();
            var m = CFileMode.Read;

            FILE f = null;
            using (f = new FILE(s, m, Encoding.Default))
            {
                Assert.Same(s, f.Source);
                Assert.Same(Encoding.Default, f.Encoding);
                Assert.Equal(m, f.Mode);
                Assert.Equal(0, f.Position);
            }
            Assert.Null(f.Source);
            Assert.Null(f.Encoding);
            Assert.Null(f.Mode);
            Assert.Equal(-1, f.Position);

            f.Dispose();
            Assert.Null(f.Source);
            Assert.Null(f.Encoding);
            Assert.Null(f.Mode);
            Assert.Equal(-1, f.Position);
        }

        [Fact]
        public void Reopen()
        {
            var s1 = new MemoryStream();
            var m = CFileMode.Read;

            var f = new FILE(s1, m, Encoding.Default);
            Assert.Same(s1, f.Source);
            Assert.Same(Encoding.Default, f.Encoding);
            Assert.Equal(m, f.Mode);

            var s2 = new MemoryStream();

            f.Reopen(s2, m, Encoding.UTF8);
            Assert.Same(s2, f.Source);
            Assert.Same(Encoding.UTF8, f.Encoding);
            Assert.Equal(m, f.Mode);

            Assert.Throws<ArgumentNullException>(() => f.Reopen(s1, m, null));
            Assert.Throws<ArgumentNullException>(() => f.Reopen(null, m, Encoding.Default));

            s1 = new MemoryStream();
            m = CFileMode.Read | CFileMode.Binary;
            f.Reopen(s1, m, Encoding.Default);
            Assert.Same(s1, f.Source);
            Assert.Same(Encoding.Default, f.Encoding);
            Assert.Equal(m, f.Mode);

            s1 = new MemoryStream();
            f.Reopen(s1, m, null);
            Assert.Same(s1, f.Source);
            Assert.Null(f.Encoding);
            Assert.Equal(m, f.Mode);

            Assert.Throws<ArgumentNullException>(() => f.Reopen(null, m, Encoding.Default));
        }

        [Fact]
        public void WriteString()
        {
            var stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read, Encoding.ASCII))
            {
                Assert.Equal(-1, file.Write("Été\nTest"));
                Assert.Equal(0, file.Position);
            }

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read | CFileMode.Update, Encoding.ASCII))
            {
                Assert.Equal(9, file.Write("Été\nTest"));
                Assert.Equal(9, file.Position);
            }
            Assert.Equal(new byte[] { 63, 116, 63, 13, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write, Encoding.UTF8))
            {
                Assert.Equal(11, file.Write("Été\nTest"));
                Assert.Equal(11, file.Position);
            }
            Assert.Equal(new byte[] { 195, 137, 116, 195, 169, 13, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Append, Encoding.UTF8))
            {
                Assert.Equal(11, file.Write("Été\nTest"));
                Assert.Equal(11, file.Position);
            }
            Assert.Equal(new byte[] { 195, 137, 116, 195, 169, 13, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read | CFileMode.Update | CFileMode.Binary, Encoding.ASCII))
            {
                Assert.Equal(8, file.Write("Été\nTest"));
                Assert.Equal(8, file.Position);
            }
            Assert.Equal(new byte[] { 63, 116, 63, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write | CFileMode.Binary, Encoding.UTF8))
            {
                Assert.Equal(10, file.Write("Été\nTest"));
                Assert.Equal(10, file.Position);
            }
            Assert.Equal(new byte[] { 195, 137, 116, 195, 169, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Append | CFileMode.Binary, Encoding.UTF8))
            {
                Assert.Equal(10, file.Write("Été\nTest"));
                Assert.Equal(10, file.Position);
            }
            Assert.Equal(new byte[] { 195, 137, 116, 195, 169, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write, Encoding.UTF8))
            {
                Assert.Equal(0, file.Write(""));
                Assert.Equal(0, file.Position);
            }
            Assert.Equal(new byte[] { }, stream.ToArray());

            FILE f;
            stream = new MemoryStream();
            using (f = new FILE(stream, CFileMode.Read, Encoding.UTF8))
            {
                Assert.Equal(-1, f.Write(""));
                Assert.Equal(0, f.Position);
            }
            Assert.Equal(-1, f.Write(""));
            Assert.Equal(-1, f.Position);
        }

        [Fact]
        public void WriteBytes()
        {
            var buffer = Encoding.UTF8.GetBytes("Été\nTest");
            var stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read, Encoding.ASCII))
            {
                Assert.Equal(-1, file.Write(buffer, 0, buffer.Length));
                Assert.Equal(0, file.Position);
            }

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read | CFileMode.Update, Encoding.ASCII))
            {
                Assert.Equal(10, file.Write(buffer, 0, buffer.Length));
                Assert.Equal(10, file.Position);
            }
            Assert.Equal(buffer, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write, Encoding.UTF8))
            {
                Assert.Equal(10, file.Write(buffer, 0, buffer.Length));
                Assert.Equal(10, file.Position);
            }
            Assert.Equal(buffer, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Append, Encoding.UTF8))
            {
                Assert.Equal(10, file.Write(buffer, 0, buffer.Length));
                Assert.Equal(10, file.Position);
            }
            Assert.Equal(buffer, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read | CFileMode.Update | CFileMode.Binary, Encoding.ASCII))
            {
                Assert.Equal(10, file.Write(buffer, 0, buffer.Length));
                Assert.Equal(10, file.Position);
            }
            Assert.Equal(buffer, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write | CFileMode.Binary, Encoding.UTF8))
            {
                Assert.Equal(10, file.Write(buffer, 0, buffer.Length));
                Assert.Equal(10, file.Position);
            }
            Assert.Equal(buffer, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Append | CFileMode.Binary, Encoding.UTF8))
            {
                Assert.Equal(10, file.Write(buffer, 0, buffer.Length));
                Assert.Equal(10, file.Position);
            }
            Assert.Equal(buffer, stream.ToArray());

            stream = new MemoryStream();
            buffer = new byte[0];
            using (var file = new FILE(stream, CFileMode.Write, Encoding.UTF8))
            {
                Assert.Equal(0, file.Write(buffer, 0, buffer.Length));
                Assert.Equal(0, file.Position);
            }
            Assert.Equal(new byte[] { }, stream.ToArray());

        }

        [Fact]
        public void Write()
        {
            var stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write, Encoding.UTF8))
            {
                Assert.Equal(5, file.Write("Été"));
                Assert.Equal(5, file.Position);
                Assert.Equal(2, file.Write(new int[] { 1, 2 }, 0, 2));
                Assert.Equal(13, file.Position);
                Assert.Equal(3, file.Write(new char[] { 'É', 't', 'é' }, 0, 3));
                Assert.Equal(18, file.Position);
                Assert.Equal(2, file.Write(new uint[] { 3, 4 }, 0, 2));
                Assert.Equal(26, file.Position);
                Assert.Equal(2, file.Write(new byte[] { 5, 6 }, 0, 2));
                Assert.Equal(28, file.Position);
                Assert.Equal(2, file.Write(new sbyte[] { 7, 8 }, 0, 2));
                Assert.Equal(30, file.Position);
                Assert.Equal(2, file.Write(new short[] { 9, 10 }, 0, 2));
                Assert.Equal(34, file.Position);
                Assert.Equal(2, file.Write(new ushort[] { 11, 12 }, 0, 2));
                Assert.Equal(38, file.Position);
                Assert.Equal(2, file.Write(new long[] { 13, 14 }, 0, 2));
                Assert.Equal(54, file.Position);
                Assert.Equal(2, file.Write(new ulong[] { 15, 17 }, 0, 2));
                Assert.Equal(70, file.Position);
                Assert.Equal(2, file.Write(new float[] { 13.4f, 14.5f }, 0, 2));
                Assert.Equal(78, file.Position);
                Assert.Equal(2, file.Write(new double[] { 15.6f, 16.7f }, 0, 2));
                Assert.Equal(94, file.Position);
            }
            int skip = 0;// + 5 + 4 + 4 + 5 + 4 + 4 + 1 + 1 + 1 + 1 + 2 + 2 + 2 + 2 + 8 + 8 + 4 + 4 + 8 + 8;
            byte[] expected = new byte[] {
                195, 137, 116, 195, 169,
                1,0,0,0, 2,0,0,0,
                195, 137, 116, 195, 169,
                3,0,0,0, 4,0,0,0,
                5, 6,
                7, 8,
                9,0, 10,0,
                11,0, 12,0,
                13,0,0,0,0,0,0,0,
                14,0,0,0,0,0,0,0,
                15,0,0,0,0,0,0,0,
                17,0,0,0,0,0,0,0,
                102, 102, 86, 65,
                0, 0, 104, 65,
                0, 0, 0, 64, 51, 51, 47, 64,
                0, 0, 0, 64, 51, 179, 48, 64,
            };
            Assert.Equal(expected.Skip(skip), stream.ToArray().Skip(skip));

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read, Encoding.UTF8))
            {
                Assert.Equal(-1, file.Write("Été"));
                Assert.Equal(-1, file.Write(new int[] { 1, 2 }, 0, 2));
                Assert.Equal(-1, file.Write(new char[] { 'É', 't', 'é' }, 0, 3));
                Assert.Equal(-1, file.Write(new uint[] { 3, 4 }, 0, 2));
                Assert.Equal(-1, file.Write(new byte[] { 5, 6 }, 0, 2));
                Assert.Equal(-1, file.Write(new sbyte[] { 7, 8 }, 0, 2));
                Assert.Equal(-1, file.Write(new short[] { 9, 10 }, 0, 2));
                Assert.Equal(-1, file.Write(new ushort[] { 11, 12 }, 0, 2));
                Assert.Equal(-1, file.Write(new long[] { 13, 14 }, 0, 2));
                Assert.Equal(-1, file.Write(new ulong[] { 15, 17 }, 0, 2));
                Assert.Equal(-1, file.Write(new float[] { 13.4f, 14.5f }, 0, 2));
                Assert.Equal(-1, file.Write(new double[] { 15.6f, 16.7f }, 0, 2));
            }
        }

        char[] ReadCharsFromFile(byte[] bytes, CFileMode mode, Encoding encoding)
        {
            var stream = new MemoryStream(bytes);
            List<char> result = new List<char>();
            using (var file = new FILE(stream, mode, encoding))
            {
                int c;
                while ((c = file.Read()) != C.EOF)
                    result.Add((char)c);
            }
            return result.ToArray();
        }

        [Fact]
        public void ReadChar()
        {
            // ASCII text
            var bytes = new byte[] { 63, 116, 63, 13, 10, 84, 101, 115, 116 };
            var actual = ReadCharsFromFile(bytes, CFileMode.Read, Encoding.ASCII);
            Assert.Equal(new char[] { '?', 't', '?', '\n', 'T', 'e', 's', 't' }, actual);

            actual = ReadCharsFromFile(bytes, CFileMode.Read | CFileMode.Binary, Encoding.ASCII);
            Assert.Equal(new char[] { '?', 't', '?', '\r', '\n', 'T', 'e', 's', 't' }, actual);

            actual = ReadCharsFromFile(bytes, CFileMode.Read, Encoding.UTF8);
            Assert.Equal(new char[] { '?', 't', '?', '\n', 'T', 'e', 's', 't' }, actual);

            actual = ReadCharsFromFile(bytes, CFileMode.Read | CFileMode.Binary, Encoding.UTF8);
            Assert.Equal(new char[] { '?', 't', '?', '\r', '\n', 'T', 'e', 's', 't' }, actual);

            // UTF-8
            bytes = new byte[] { 195, 137, 116, 195, 169, 13, 10, 84, 101, 115, 116 };
            actual = ReadCharsFromFile(bytes, CFileMode.Read, Encoding.ASCII);
            Assert.Equal(new char[] { '?', '?', 't', '?', '?', '\n', 'T', 'e', 's', 't' }, actual);

            actual = ReadCharsFromFile(bytes, CFileMode.Read | CFileMode.Binary, Encoding.ASCII);
            Assert.Equal(new char[] { '?', '?', 't', '?', '?', '\r', '\n', 'T', 'e', 's', 't' }, actual);

            actual = ReadCharsFromFile(bytes, CFileMode.Read, Encoding.UTF8);
            Assert.Equal(new char[] { 'É', 't', 'é', '\n', 'T', 'e', 's', 't' }, actual);

            actual = ReadCharsFromFile(bytes, CFileMode.Read | CFileMode.Binary, Encoding.UTF8);
            Assert.Equal(new char[] { 'É', 't', 'é', '\r', '\n', 'T', 'e', 's', 't' }, actual);

            // New line
            bytes = new byte[] { 84, 13, 10, 13, 10, 84 };
            actual = ReadCharsFromFile(bytes, CFileMode.Read, Encoding.ASCII);
            Assert.Equal(new char[] { 'T', '\n', '\n', 'T' }, actual);
            actual = ReadCharsFromFile(bytes, CFileMode.Read | CFileMode.Binary, Encoding.ASCII);
            Assert.Equal(new char[] { 'T', '\r', '\n', '\r', '\n', 'T' }, actual);

            bytes = new byte[] { 84, 13, 13, 84 };
            actual = ReadCharsFromFile(bytes, CFileMode.Read, Encoding.ASCII);
            Assert.Equal(new char[] { 'T', '\n', '\n', 'T' }, actual);
            actual = ReadCharsFromFile(bytes, CFileMode.Read | CFileMode.Binary, Encoding.ASCII);
            Assert.Equal(new char[] { 'T', '\r', '\r', 'T' }, actual);

            bytes = new byte[] { 84, 10, 10, 84 };
            actual = ReadCharsFromFile(bytes, CFileMode.Read, Encoding.ASCII);
            Assert.Equal(new char[] { 'T', '\n', '\n', 'T' }, actual);
            actual = ReadCharsFromFile(bytes, CFileMode.Read | CFileMode.Binary, Encoding.ASCII);
            Assert.Equal(new char[] { 'T', '\n', '\n', 'T' }, actual);

            bytes = new byte[] { 84, 13 };
            actual = ReadCharsFromFile(bytes, CFileMode.Read, Encoding.ASCII);
            Assert.Equal(new char[] { 'T', '\n' }, actual);
            actual = ReadCharsFromFile(bytes, CFileMode.Read | CFileMode.Binary, Encoding.ASCII);
            Assert.Equal(new char[] { 'T', '\r' }, actual);

            // 
            FILE f;
            var stream = new MemoryStream(new byte[0]);
            using (f = new FILE(stream, CFileMode.Read, Encoding.UTF8))
                Assert.Equal(-1, f.Read());
            Assert.Equal(-1, f.Read());
        }

        [Fact]
        public void Read()
        {
            byte[] data = new byte[] {
                195, 137, 116, 195, 169,
                1,0,0,0, 2,0,0,0,
                195, 137, 116, 195, 169,
                3,0,0,0, 4,0,0,0,
                5, 6,
                7, 8,
                9,0, 10,0,
                11,0, 12,0,
                13,0,0,0,0,0,0,0,
                14,0,0,0,0,0,0,0,
                15,0,0,0,0,0,0,0,
                17,0,0,0,0,0,0,0,
                102, 102, 86, 65,
                0, 0, 104, 65,
                0, 0, 0, 64, 51, 51, 47, 64,
                0, 0, 0, 64, 51, 179, 48, 64,
            };

            var stream = new MemoryStream(data);
            using (var file = new FILE(stream, CFileMode.Read, Encoding.UTF8))
            {
                Assert.Equal("Été", file.ReadString(3));
                Assert.Equal(5, file.Position);

                int[] b1 = new int[2];
                Assert.Equal(2, file.Read(b1, 0, 2));
                Assert.Equal(new int[] { 1, 2 }, b1);
                Assert.Equal(13, file.Position);

                char[] b2 = new char[3];
                Assert.Equal(3, file.Read(b2, 0, 3));
                Assert.Equal(new char[] { 'É', 't', 'é' }, b2);
                Assert.Equal(18, file.Position);

                uint[] b3 = new uint[2];
                Assert.Equal(2, file.Read(b3, 0, 2));
                Assert.Equal(new uint[] { 3, 4 }, b3);
                Assert.Equal(26, file.Position);

                byte[] b4 = new byte[2];
                Assert.Equal(2, file.Read(b4, 0, 2));
                Assert.Equal(new byte[] { 5, 6 }, b4);
                Assert.Equal(28, file.Position);

                sbyte[] b5 = new sbyte[2];
                Assert.Equal(2, file.Read(b5, 0, 2));
                Assert.Equal(new sbyte[] { 7, 8 }, b5);
                Assert.Equal(30, file.Position);

                short[] b6 = new short[2];
                Assert.Equal(2, file.Read(b6, 0, 2));
                Assert.Equal(new short[] { 9, 10 }, b6);
                Assert.Equal(34, file.Position);

                ushort[] b7 = new ushort[2];
                Assert.Equal(2, file.Read(b7, 0, 2));
                Assert.Equal(new ushort[] { 11, 12 }, b7);
                Assert.Equal(38, file.Position);

                long[] b8 = new long[2];
                Assert.Equal(2, file.Read(b8, 0, 2));
                Assert.Equal(new long[] { 13, 14 }, b8);
                Assert.Equal(54, file.Position);

                ulong[] b9 = new ulong[2];
                Assert.Equal(2, file.Read(b9, 0, 2));
                Assert.Equal(new ulong[] { 15, 17 }, b9);
                Assert.Equal(70, file.Position);

                float[] b10 = new float[2];
                Assert.Equal(2, file.Read(b10, 0, 2));
                Assert.Equal(new float[] { 13.4f, 14.5f }, b10);
                Assert.Equal(78, file.Position);

                double[] b11 = new double[2];
                Assert.Equal(2, file.Read(b11, 0, 2));
                Assert.Equal(new double[] { 15.6f, 16.7f }, b11);
                Assert.Equal(94, file.Position);
            }

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write, Encoding.UTF8))
            {
                Assert.Equal(null, file.ReadString(3));
                int[] b1 = new int[2];
                Assert.Equal(-1, file.Read(b1, 0, 2));
                char[] b2 = new char[3];
                Assert.Equal(-1, file.Read(b2, 0, 3));
                uint[] b3 = new uint[2];
                Assert.Equal(-1, file.Read(b3, 0, 2));
                byte[] b4 = new byte[2];
                Assert.Equal(-1, file.Read(b4, 0, 2));
                sbyte[] b5 = new sbyte[2];
                Assert.Equal(-1, file.Read(b5, 0, 2));
                short[] b6 = new short[2];
                Assert.Equal(-1, file.Read(b6, 0, 2));
                ushort[] b7 = new ushort[2];
                Assert.Equal(-1, file.Read(b7, 0, 2));
                long[] b8 = new long[2];
                Assert.Equal(-1, file.Read(b8, 0, 2));
                ulong[] b9 = new ulong[2];
                Assert.Equal(-1, file.Read(b9, 0, 2));
                float[] b10 = new float[2];
                Assert.Equal(-1, file.Read(b10, 0, 2));
                double[] b11 = new double[2];
                Assert.Equal(-1, file.Read(b11, 0, 2));
            }

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read, Encoding.UTF8))
            {
                Assert.Equal(string.Empty, file.ReadString(3));
                int[] b1 = new int[2];
                Assert.Equal(0, file.Read(b1, 0, 2));
                char[] b2 = new char[3];
                Assert.Equal(0, file.Read(b2, 0, 3));
                uint[] b3 = new uint[2];
                Assert.Equal(0, file.Read(b3, 0, 2));
                byte[] b4 = new byte[2];
                Assert.Equal(0, file.Read(b4, 0, 2));
                sbyte[] b5 = new sbyte[2];
                Assert.Equal(0, file.Read(b5, 0, 2));
                short[] b6 = new short[2];
                Assert.Equal(0, file.Read(b6, 0, 2));
                ushort[] b7 = new ushort[2];
                Assert.Equal(0, file.Read(b7, 0, 2));
                long[] b8 = new long[2];
                Assert.Equal(0, file.Read(b8, 0, 2));
                ulong[] b9 = new ulong[2];
                Assert.Equal(0, file.Read(b9, 0, 2));
                float[] b10 = new float[2];
                Assert.Equal(0, file.Read(b10, 0, 2));
                double[] b11 = new double[2];
                Assert.Equal(0, file.Read(b11, 0, 2));
            }
        }

        [Fact]
        public void UnreadChar()
        {
            var bytes = new byte[] { 195, 137, 116, 195, 169, 13, 10, 84, 101, 115, 116 };
            var stream = new MemoryStream(bytes);
            using (var file = new FILE(stream, CFileMode.Read, Encoding.UTF8))
            {
                Assert.Equal('É', file.Read());
                Assert.Equal('t', file.Read());
                Assert.True(file.UnreadChar('T'));
                Assert.True(file.UnreadChar('É'));
                Assert.Equal('É', file.Read());
                Assert.Equal('T', file.Read());
                Assert.Equal('é', file.Read());
            }

            stream = new MemoryStream(bytes);
            using (var file = new FILE(stream, CFileMode.Write, Encoding.UTF8))
            {
                Assert.Equal(C.EOF, file.Read());
                Assert.False(file.UnreadChar('T'));
            }
        }

    }
}
