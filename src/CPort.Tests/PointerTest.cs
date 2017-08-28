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

        [Fact]
        public void TestGetHashCode()
        {
            int[] source = Enumerable.Range(1, 10).ToArray();

            var p = new Pointer<int>(source);
            Assert.Equal(source.GetHashCode() ^ 0, p.GetHashCode());

            p = new Pointer<int>(source, 6);
            Assert.Equal(source.GetHashCode() ^ 6, p.GetHashCode());

            p = new Pointer<int>(source, 50);
            Assert.Equal(source.GetHashCode() ^ 50, p.GetHashCode());

            p = new Pointer<int>(source, -50);
            Assert.Equal(source.GetHashCode() ^ 0, p.GetHashCode());

            p = new Pointer<int>(null, 0);
            Assert.Equal(0, p.GetHashCode());
        }

        [Fact]
        public void TestEquals()
        {
            int[] source = Enumerable.Range(1, 10).ToArray();

            var p1 = new Pointer<int>(source);
            Assert.True(p1.Equals(source));

            var p2 = new Pointer<int>(source, 6);
            Assert.False(p2.Equals(source));

            var p3 = new Pointer<int>(null, 0);
            Assert.False(p3.Equals(source));

            Assert.True(p1.Equals(new Pointer<int>(source, 0)));
            Assert.False(p1.Equals(new Pointer<int>(source, 6)));
            Assert.False(p2.Equals(new Pointer<int>(source, 0)));
            Assert.True(p2.Equals(new Pointer<int>(source, 6)));
            Assert.False(p3.Equals(new Pointer<int>(source, 0)));
            Assert.False(p3.Equals(new Pointer<int>(source, 6)));

            Assert.False(p1.Equals(null));
            Assert.False(p1.Equals(123));
        }

        [Fact]
        public void Casts()
        {
            int[] array = Enumerable.Range(1, 10).ToArray();
            List<int> list = new List<int>(array);

            Pointer<int> p = array;
            Assert.Same(array, p.Source);
            Assert.Equal(0, p.Index);

            p = list;
            Assert.Same(list, p.Source);
            Assert.Equal(0, p.Index);

            int[] narray = (int[])p;
            Assert.Equal(narray, array);

            List<int> nlist = (List<int>)p;
            Assert.Equal(nlist, list);

            p = new Pointer<int>(array, 5);
            narray = (int[])p;
            Assert.Equal(narray, array.Skip(5));
            nlist = (List<int>)p;
            Assert.Equal(nlist, list.Skip(5));

            p = new Pointer<int>();
            narray = (int[])p;
            Assert.Null(narray);
            nlist = (List<int>)p;
            Assert.Null(nlist);
        }

    }
}
