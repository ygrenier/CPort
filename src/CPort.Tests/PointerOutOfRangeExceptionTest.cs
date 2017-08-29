using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CPort.Tests
{
    public class PointerOutOfRangeExceptionTest
    {
        [Fact]
        public void Create()
        {
            var ex = new PointerOutOfRangeException();
            Assert.Equal("This pointer index value is out of range of the source.", ex.Message);
            Assert.Equal(null, ex.Index);

            ex = new PointerOutOfRangeException("Message");
            Assert.Equal("Message", ex.Message);
            Assert.Equal(null, ex.Index);

            ex = new PointerOutOfRangeException(123);
            Assert.Equal("This pointer index (123) value is out of range of the source.", ex.Message);
            Assert.Equal(123, ex.Index);

            ex = new PointerOutOfRangeException(321, "Message");
            Assert.Equal("Message", ex.Message);
            Assert.Equal(321, ex.Index);
        }
    }
}
