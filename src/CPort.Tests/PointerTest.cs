using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CPort.Tests
{
    public class PointerTest
    {
        [Fact]
        public void Create()
        {
            Pointer<int> p = new Pointer<int>();
            Assert.True(p.IsNull);
            Assert.Null(p.Source);
            Assert.Equal(0, p.Index);

            p = new Pointer<int>(10);
            Assert.False(p.IsNull);
            Assert.NotNull(p.Source);
            Assert.Equal(10, p.Source.Count);
            Assert.Equal(0, p.Index);

            int[] source = new int[] { 1, 2, 3 };
            p = new Pointer<int>(source);
            Assert.False(p.IsNull);
            Assert.NotNull(p.Source);
            Assert.Same(source, p.Source);
            Assert.Equal(0, p.Index);

            p = new Pointer<int>(source, 4);
            Assert.False(p.IsNull);
            Assert.NotNull(p.Source);
            Assert.Same(source, p.Source);
            Assert.Equal(4, p.Index);

            p = new Pointer<int>(source, -4);
            Assert.False(p.IsNull);
            Assert.NotNull(p.Source);
            Assert.Same(source, p.Source);
            Assert.Equal(0, p.Index);

            p = new Pointer<int>(null);
            Assert.True(p.IsNull);
            Assert.Null(p.Source);
            Assert.Equal(0, p.Index);

            p = new Pointer<int>(null, 4);
            Assert.True(p.IsNull);
            Assert.Null(p.Source);
            Assert.Equal(0, p.Index);
        }

        [Fact]
        public void AsEnumerable()
        {
            int[] source = Enumerable.Range(1, 10).ToArray();

            var p = new Pointer<int>(source);
            Assert.Equal(source, p.ToArray());

            p = new Pointer<int>(source, 6);
            Assert.Equal(new int[] { 7, 8, 9, 10 }, p.ToArray());

            p = new Pointer<int>(source, 50);
            Assert.Equal(new int[] { }, p.ToArray());

            p = new Pointer<int>(source, -50);
            Assert.Equal(source, p.ToArray());

            IEnumerable penum = p;
            Assert.Equal(source, penum.OfType<int>().ToArray());

            p = new Pointer<int>(null, 0);
            Assert.Equal(new int[] { }, p.ToArray());
        }

    }
}
