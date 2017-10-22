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

        [Fact]
        public void CopyTo()
        {
            int[] source = new int[] { 1, 3, 5, 7 };
            Pointer<int> dest = new int[10];

            source.CopyTo(dest + 5, 2);
            Assert.Equal(new int[] { 0, 0, 0, 0, 0, 1, 3, 0, 0, 0 }, dest.Source);
            source.CopyTo(dest + 3);
            Assert.Equal(new int[] { 0, 0, 0, 1, 3, 5, 7, 0, 0, 0 }, dest.Source);
        }
    }
}
