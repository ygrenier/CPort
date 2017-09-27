using static CPort.C;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace CPort.Tests
{
    public class SystemHostTest
    {

        [Fact]
        public void Test_C_System()
        {
            // Get default system host
            var s1 = C.SystemHost;
            Assert.NotNull(s1);
            Assert.IsType<DefaultSystemHost>(s1);

            // Reset the current system host
            C.SetSystemHost(null);
            var s2 = C.SystemHost;
            Assert.NotNull(s2);
            Assert.IsType<DefaultSystemHost>(s2);
            Assert.NotSame(s1, s2);

            // Define custom system host
            var mHost = new Mock<ISystemHost>();
            var host = mHost.Object;
            C.SetSystemHost(host);
            var s3 = C.SystemHost;
            Assert.NotNull(s3);
            Assert.Same(host, s3);
        }

    }
}
