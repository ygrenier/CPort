using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CPort.Tests
{
    public class NulStreamTest
    {
        [Fact]
        public void Tests()
        {
            var stream = new NulStream();

            stream.Flush();
            Assert.Equal(0, stream.Read(null, 0, 0));
            stream.Write(null, 0, 0);

            Assert.True(stream.CanRead);
            Assert.True(stream.CanWrite);
            Assert.False(stream.CanSeek);
            Assert.Equal(0, stream.Position);
            Assert.Equal(0, stream.Length);

            Assert.Throws<InvalidOperationException>(() => stream.Position = 0);
            Assert.Throws<InvalidOperationException>(() => stream.Seek(0, System.IO.SeekOrigin.Begin));
            Assert.Throws<InvalidOperationException>(() => stream.SetLength(0));
        }
    }
}
