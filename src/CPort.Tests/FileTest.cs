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

            Assert.Throws<ArgumentNullException>(() => new FILE(s, m, null));
            Assert.Throws<ArgumentNullException>(() => new FILE(null, m, Encoding.Default));

            m = CFileMode.Read | CFileMode.Binary;
            f = new FILE(s, m, Encoding.Default);
            Assert.Same(s, f.Source);
            Assert.Same(Encoding.Default, f.Encoding);
            Assert.Equal(m, f.Mode);

            f = new FILE(s, m, null);
            Assert.Same(s, f.Source);
            Assert.Null(f.Encoding);
            Assert.Equal(m, f.Mode);

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
            }
            Assert.Null(f.Source);
            Assert.Null(f.Encoding);
            Assert.Null(f.Mode);

            f.Dispose();
            Assert.Null(f.Source);
            Assert.Null(f.Encoding);
            Assert.Null(f.Mode);

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

            m = CFileMode.Read | CFileMode.Binary;
            f.Reopen(s1, m, Encoding.Default);
            Assert.Same(s1, f.Source);
            Assert.Same(Encoding.Default, f.Encoding);
            Assert.Equal(m, f.Mode);

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
                Assert.Equal(-1, file.Write("Été\nTest"));

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read | CFileMode.Update, Encoding.ASCII))
                Assert.Equal(9, file.Write("Été\nTest"));
            Assert.Equal(new byte[] { 63, 116, 63, 13, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write, Encoding.UTF8))
                Assert.Equal(11, file.Write("Été\nTest"));
            Assert.Equal(new byte[] { 195, 137, 116, 195, 169, 13, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Append, Encoding.UTF8))
                Assert.Equal(11, file.Write("Été\nTest"));
            Assert.Equal(new byte[] { 195, 137, 116, 195, 169, 13, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read | CFileMode.Update | CFileMode.Binary, Encoding.ASCII))
                Assert.Equal(8, file.Write("Été\nTest"));
            Assert.Equal(new byte[] { 63, 116, 63, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write | CFileMode.Binary, Encoding.UTF8))
                Assert.Equal(10, file.Write("Été\nTest"));
            Assert.Equal(new byte[] { 195, 137, 116, 195, 169, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Append | CFileMode.Binary, Encoding.UTF8))
                Assert.Equal(10, file.Write("Été\nTest"));
            Assert.Equal(new byte[] { 195, 137, 116, 195, 169, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write, Encoding.UTF8))
                Assert.Equal(0, file.Write(""));
            Assert.Equal(new byte[] { }, stream.ToArray());

        }
    }
}
