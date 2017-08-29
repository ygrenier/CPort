using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CPort.Tests
{
    public class PointerNullExceptionTest
    {
        [Fact]
        public void Create()
        {
            var ex = new PointerNullException();
            Assert.Equal("This pointer is null.", ex.Message);

            ex = new PointerNullException("Message");
            Assert.Equal("Message", ex.Message);

        }
    }
}
