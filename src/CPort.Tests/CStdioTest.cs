using static CPort.C;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.IO;

namespace CPort.Tests
{
    public class CStdioTest
    {

        [Fact]
        public void Fopen()
        {
            C.SetSystemHost(null);
            Assert.Null(fopen("file", "r"));
            Assert.Null(fopen(NULL, "r"));
            Assert.Null(fopen("file", NULL));

            var mHost = new Mock<ISystemHost>();
            mHost.Setup(h => h.OpenFile(It.IsAny<string>(), It.IsAny<CFileMode>()))
                .Returns(() => Tuple.Create<Stream, Encoding>(new MemoryStream(), Encoding.ASCII));
            C.SetSystemHost(mHost.Object);

            var file = fopen("file", "r");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.ASCII, file.Encoding);
            Assert.Equal(CFileMode.Read, file.Mode);

            file = fopen("file", "w");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.ASCII, file.Encoding);
            Assert.Equal(CFileMode.Write, file.Mode);

            file = fopen("file", "a");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.ASCII, file.Encoding);
            Assert.Equal(CFileMode.Append, file.Mode);

            file = fopen("file", "r+");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.ASCII, file.Encoding);
            Assert.Equal(CFileMode.Read | CFileMode.Update, file.Mode);

            file = fopen("file", "w+");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.ASCII, file.Encoding);
            Assert.Equal(CFileMode.Write | CFileMode.Update, file.Mode);

            file = fopen("file", "a+");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.ASCII, file.Encoding);
            Assert.Equal(CFileMode.Append | CFileMode.Update, file.Mode);

            file = fopen("file", "rb");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.ASCII, file.Encoding);
            Assert.Equal(CFileMode.Read | CFileMode.Binary, file.Mode);

            file = fopen("file", "wb");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.ASCII, file.Encoding);
            Assert.Equal(CFileMode.Write | CFileMode.Binary, file.Mode);

            file = fopen("file", "ab");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.ASCII, file.Encoding);
            Assert.Equal(CFileMode.Append | CFileMode.Binary, file.Mode);

            file = fopen("file", "r+b");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.ASCII, file.Encoding);
            Assert.Equal(CFileMode.Read | CFileMode.Update | CFileMode.Binary, file.Mode);

            file = fopen("file", "w+b");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.ASCII, file.Encoding);
            Assert.Equal(CFileMode.Write | CFileMode.Update | CFileMode.Binary, file.Mode);

            file = fopen("file", "a+b");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.ASCII, file.Encoding);
            Assert.Equal(CFileMode.Append | CFileMode.Update | CFileMode.Binary, file.Mode);

            mHost.Setup(h => h.OpenFile(It.IsAny<string>(), It.IsAny<CFileMode>()))
                .Returns(() => Tuple.Create<Stream, Encoding>(new MemoryStream(), null));
            mHost.SetupGet(h => h.DefaultFileEncoding).Returns(Encoding.UTF8);
            C.SetSystemHost(mHost.Object);
            file = fopen("file", "a");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.UTF8, file.Encoding);
            Assert.Equal(CFileMode.Append, file.Mode);
        }

        [Fact]
        public void Freopen()
        {
            var mHost = new Mock<ISystemHost>();
            mHost.Setup(h => h.OpenFile(It.IsAny<string>(), It.IsAny<CFileMode>()))
                .Returns(() => Tuple.Create<Stream, Encoding>(new MemoryStream(), Encoding.ASCII));
            C.SetSystemHost(mHost.Object);

            var file1 = fopen("file", "r+");
            Assert.NotNull(file1);
            Assert.IsType<MemoryStream>(file1.Source);
            Assert.Same(Encoding.ASCII, file1.Encoding);
            Assert.Equal(CFileMode.Read | CFileMode.Update, file1.Mode);

            var file2 = freopen("file", "w+", file1);
            Assert.NotNull(file2);
            Assert.Same(file1, file2);
            Assert.IsType<MemoryStream>(file2.Source);
            Assert.Same(Encoding.ASCII, file2.Encoding);
            Assert.Equal(CFileMode.Write | CFileMode.Update, file2.Mode);

            mHost.Setup(h => h.OpenFile(It.IsAny<string>(), It.IsAny<CFileMode>()))
                .Returns(() => Tuple.Create<Stream, Encoding>(new MemoryStream(), null));
            mHost.SetupGet(h => h.DefaultFileEncoding).Returns(Encoding.UTF8);
            C.SetSystemHost(mHost.Object);
            file2 = freopen("file", "a+", file1);
            Assert.NotNull(file2);
            Assert.IsType<MemoryStream>(file2.Source);
            Assert.Same(Encoding.UTF8, file2.Encoding);
            Assert.Equal(CFileMode.Append | CFileMode.Update, file2.Mode);

            C.SetSystemHost(null);
            Assert.Null(freopen("file", "r", file1));
            Assert.Null(freopen(NULL, "r", file1));
            Assert.Null(freopen("file", NULL, file1));
        }

        [Fact]
        public void Fclose()
        {
            var file = new FILE(new MemoryStream(), CFileMode.Read, Encoding.ASCII);

            Assert.Equal(0, fclose(file));
            Assert.Equal(EOF, fclose(file));
            Assert.Equal(EOF, fclose(null));
        }

        [Fact]
        public void Fflush()
        {
            var file = new FILE(new MemoryStream(new byte[] { 1, 2, 3 }), CFileMode.Read, Encoding.ASCII);
            Assert.Equal(0, fflush(file));
            file.Dispose();
            Assert.Equal(EOF, fflush(file));
            file = null;
            Assert.Equal(EOF, fflush(file));
        }

        [Fact]
        public void Fprintf()
        {
            using (var stream = new MemoryStream())
            {
                using (var file = new FILE(stream, CFileMode.Write, Encoding.ASCII))
                {
                    fprintf(file, "%s:%4d", "Test", 123);
                }
                Assert.Equal(Encoding.ASCII.GetBytes("Test: 123"), stream.ToArray());
            }
        }

        [Fact]
        public void Sprintf()
        {
            var buff = new PChar(15);
            Assert.Equal(9, sprintf(buff + 1, "%s:%4d", "Test", 123));
            Assert.Equal(new char[] { '\0', 'T', 'e', 's', 't', ':', ' ', '1', '2', '3', '\0', '\0', '\0', '\0', '\0' }, buff.Source);

            Assert.Equal(0, sprintf(buff + 2, "", "Test", 123));
            Assert.Equal(new char[] { '\0', 'T', 'e', 's', 't', ':', ' ', '1', '2', '3', '\0', '\0', '\0', '\0', '\0' }, buff.Source);
        }

        [Fact]
        public void Fgetc()
        {
            var stream = new MemoryStream(new byte[] { 84, 13, 10, 84 });
            using (var file = new FILE(stream, CFileMode.Read, Encoding.ASCII))
            {
                Assert.Equal('T', fgetc(file));
                Assert.Equal('\n', getc(file));
                Assert.Equal('T', getc(file));
                Assert.Equal(EOF, fgetc(file));
            }

            stream = new MemoryStream(new byte[] { 84, 13, 10, 84 });
            using (var file = new FILE(stream, CFileMode.Write, Encoding.ASCII))
            {
                Assert.Equal(EOF, fgetc(file));
                Assert.Equal(EOF, fgetc(null));
            }
        }

        [Fact]
        public void Fgets()
        {
            var stream = new MemoryStream(new byte[] { 84, 13, 10, 84 });
            PChar s = new PChar(255);
            using (var file = new FILE(stream, CFileMode.Read, Encoding.ASCII))
            {
                Assert.Equal("T\n", fgets(s, 255, file).GetString());
                Assert.Equal("T", fgets(s, 255, file).GetString());
                Assert.Equal(NULL, fgets(s, 255, file));
            }

            stream = new MemoryStream(new byte[] { 84, 13, 10, 84 });
            using (var file = new FILE(stream, CFileMode.Read | CFileMode.Binary, Encoding.ASCII))
            {
                Assert.Equal("T\r\n", fgets(s, 255, file).GetString());
                Assert.Equal("T", fgets(s, 255, file).GetString());
                Assert.Equal(NULL, fgets(s, 255, file));
            }

            stream = new MemoryStream(new byte[] { 84, 84, 84, 13, 10, 84, 84, 84 });
            using (var file = new FILE(stream, CFileMode.Read, Encoding.ASCII))
            {
                Assert.Equal("TT", fgets(s, 3, file).GetString());
                Assert.Equal("T\n", fgets(s, 3, file).GetString());
                Assert.Equal("TT", fgets(s, 3, file).GetString());
                Assert.Equal("T", fgets(s, 3, file).GetString());
                Assert.Equal(NULL, fgets(s, 3, file));
            }

            stream = new MemoryStream(new byte[] { 84, 84, 84, 13, 10, 84, 84, 84 });
            using (var file = new FILE(stream, CFileMode.Read | CFileMode.Binary, Encoding.ASCII))
            {
                Assert.Equal("TT", fgets(s, 3, file).GetString());
                Assert.Equal("T\r", fgets(s, 3, file).GetString());
                Assert.Equal("\n", fgets(s, 3, file).GetString());
                Assert.Equal("TT", fgets(s, 3, file).GetString());
                Assert.Equal("T", fgets(s, 3, file).GetString());
                Assert.Equal(NULL, fgets(s, 3, file));
            }

            stream = new MemoryStream(new byte[] { 84, 13, 10, 84 });
            using (var file = new FILE(stream, CFileMode.Write, Encoding.ASCII))
            {
                Assert.Equal(NULL, fgets(NULL, 3, file));
                Assert.Equal(NULL, fgets(s, 0, file));
                Assert.Equal(NULL, fgets(s, 3, null));
            }
        }

        [Fact]
        public void Fputc()
        {
            var stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read, Encoding.ASCII))
            {
                Assert.Equal(EOF, fputc('é', file));
                Assert.Equal(EOF, putc('\n', file));
                Assert.Equal(EOF, putc(EOF, file));
                Assert.Equal(EOF, putc('a', null));
                Assert.Equal(EOF, putc('a', file));
            }
            Assert.Equal(new byte[] { }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write, Encoding.ASCII))
            {
                Assert.Equal('é', fputc('é', file));
                Assert.Equal(10, putc('\n', file));
                Assert.Equal(EOF, putc(EOF, file));
                Assert.Equal(EOF, putc('a', null));
                Assert.Equal('a', putc('a', file));
            }
            Assert.Equal(new byte[] { 63, 13, 10, 97 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write | CFileMode.Binary, Encoding.ASCII))
            {
                Assert.Equal('é', fputc('é', file));
                Assert.Equal(10, putc('\n', file));
                Assert.Equal(EOF, putc(EOF, file));
                Assert.Equal(EOF, putc('a', null));
                Assert.Equal('a', putc('a', file));
            }
            Assert.Equal(new byte[] { 63, 10, 97 }, stream.ToArray());
        }

        [Fact]
        public void Fputs()
        {
            var stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read, Encoding.ASCII))
                Assert.Equal(-1, fputs("Été\nTest", file));

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read | CFileMode.Update, Encoding.ASCII))
                Assert.Equal(9, fputs("Été\nTest", file));
            Assert.Equal(new byte[] { 63, 116, 63, 13, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write, Encoding.UTF8))
                Assert.Equal(11, fputs("Été\nTest", file));
            Assert.Equal(new byte[] { 195, 137, 116, 195, 169, 13, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Append, Encoding.UTF8))
                Assert.Equal(11, fputs("Été\nTest", file));
            Assert.Equal(new byte[] { 195, 137, 116, 195, 169, 13, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Read | CFileMode.Update | CFileMode.Binary, Encoding.ASCII))
                Assert.Equal(8, fputs("Été\nTest", file));
            Assert.Equal(new byte[] { 63, 116, 63, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write | CFileMode.Binary, Encoding.UTF8))
                Assert.Equal(10, fputs("Été\nTest", file));
            Assert.Equal(new byte[] { 195, 137, 116, 195, 169, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Append | CFileMode.Binary, Encoding.UTF8))
                Assert.Equal(10, fputs("Été\nTest", file));
            Assert.Equal(new byte[] { 195, 137, 116, 195, 169, 10, 84, 101, 115, 116 }, stream.ToArray());

            stream = new MemoryStream();
            using (var file = new FILE(stream, CFileMode.Write, Encoding.UTF8))
            {
                Assert.Equal(0, fputs("", file));
                Assert.Equal(EOF, fputs(NULL, file));
                Assert.Equal(EOF, fputs("test", null));
            }
            Assert.Equal(new byte[] { }, stream.ToArray());
        }

        [Fact]
        public void Ungetc()
        {
            var bytes = new byte[] { 195, 137, 116, 195, 169, 13, 10, 84, 101, 115, 116 };
            var stream = new MemoryStream(bytes);
            using (var file = new FILE(stream, CFileMode.Read, Encoding.UTF8))
            {
                Assert.Equal('É', fgetc(file));
                Assert.Equal('t', fgetc(file));
                Assert.Equal('T', ungetc('T', file));
                Assert.Equal('É', ungetc('É', file));
                Assert.Equal('É', fgetc(file));
                Assert.Equal('T', fgetc(file));
                Assert.Equal('é', fgetc(file));
                Assert.Equal(EOF, ungetc(EOF, file));
                Assert.Equal(EOF, ungetc('T', null));
            }

            stream = new MemoryStream(bytes);
            using (var file = new FILE(stream, CFileMode.Write, Encoding.UTF8))
                Assert.Equal(EOF, ungetc('t', file));
        }

    }
}
