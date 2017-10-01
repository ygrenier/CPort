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

            var file = fopen("file", "w");
            Assert.NotNull(file);
            Assert.IsType<MemoryStream>(file.Source);
            Assert.Same(Encoding.ASCII, file.Encoding);
            Assert.Equal(CFileMode.Write, file.Mode);

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

    }
}
