using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CPort.Tests
{
    public class DefaultSystemHostTest
    {

        [Fact]
        public void DefaultFileEncoding()
        {
            var host = new DefaultSystemHost();
            Assert.Same(Encoding.UTF8, host.DefaultFileEncoding);
            host.DefaultFileEncoding = Encoding.ASCII;
            Assert.Same(Encoding.ASCII, host.DefaultFileEncoding);
        }

        [Fact]
        public void OpenFile()
        {
            var host = new DefaultSystemHost();
            Assert.Null(host.OpenFile("test", CFileMode.Read));
        }

    }
}
