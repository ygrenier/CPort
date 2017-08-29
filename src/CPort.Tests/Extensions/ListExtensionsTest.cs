using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CPort.Tests.Extensions
{
    public class ListExtensionsTest
    {
        [Fact]
        public void GetPointer()
        {
            int[] source = new int[] { 1, 3, 5, 7 };
            var p = source.GetPointer();
            Assert.False(p.IsNull);
            Assert.Equal(4, p.Source.Count);
            Assert.Equal(1, p[0]);
            Assert.Equal(3, p[1]);
            Assert.Equal(5, p[2]);
            Assert.Equal(7, p[3]);

            source = new int[] { };
            p = source.GetPointer();
            Assert.False(p.IsNull);
            Assert.Equal(0, p.Source.Count);

            source = null;
            p = source.GetPointer();
            Assert.True(p.IsNull);
            Assert.Null(p.Source);
        }
    }
}
