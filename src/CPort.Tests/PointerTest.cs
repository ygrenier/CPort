using static CPort.C;
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

            // Cast from NullPointer
            p = array;
            Assert.False(p.IsNull);
            p = NULL;
            Assert.True(p.IsNull);
        }

        [Fact]
        public void ValueAccess()
        {
            int[] source = Enumerable.Range(1, 10).ToArray();

            var p = new Pointer<int>(source, 5);
            Assert.Equal(6, p);
            Assert.Equal(6, p[0]);
            Assert.Equal(6, p.Value);
            Assert.Throws<PointerOutOfRangeException>(() => Assert.Equal(6, p[123]));
            Assert.True(p.TryGetValue(0, out int actual));
            Assert.Equal(6, actual);
            Assert.False(p.TryGetValue(123, out actual));
            Assert.Equal(0, actual);

            p.Value = 12;
            p[2] = 13;
            Assert.Throws<PointerOutOfRangeException>(() => p[123] = 12);
            Assert.True(p.TrySetValue(11, 0));
            Assert.False(p.TrySetValue(11, 123));

            Assert.Equal(new int[] { 1, 2, 3, 4, 5, 11, 7, 13, 9, 10 }, source);

            p = new Pointer<int>(null, 5);
            Assert.Throws<PointerNullException>(() => actual = p);
            Assert.Throws<PointerNullException>(() => actual = p.Value);
            Assert.Throws<PointerNullException>(() => actual = p[0]);
            Assert.Throws<PointerNullException>(() => p.Value = 12);
            Assert.Throws<PointerNullException>(() => p[0] = 13);
            Assert.False(p.TryGetValue(0, out actual));
            Assert.False(p.TrySetValue(11, 0));

        }

        [Fact]
        public void PointerAddSub()
        {
            int[] source = Enumerable.Range(1, 10).ToArray();

            var p1 = new Pointer<int>(source);
            Assert.Equal(0, p1.Index);
            var p2 = p1 + 4;
            Assert.Equal(0, p1.Index);
            Assert.Equal(4, p2.Index);
            var p3 = p2 - 6;
            Assert.Equal(0, p1.Index);
            Assert.Equal(4, p2.Index);
            Assert.Equal(0, p3.Index);
        }

        [Fact]
        public void PointerIncDec()
        {
            int[] source = Enumerable.Range(1, 10).ToArray();

            var p = new Pointer<int>(source);
            Assert.Equal(0, p.Index);
            Assert.Equal(0, (p++).Index);
            Assert.Equal(1, p.Index);
            Assert.Equal(2, (++p).Index);
            Assert.Equal(2, p.Index);
            Assert.Equal(2, (p--).Index);
            Assert.Equal(1, p.Index);
            Assert.Equal(0, (--p).Index);
            Assert.Equal(0, p.Index);
            Assert.Equal(0, (--p).Index);
            Assert.Equal(0, p.Index);
            Assert.Equal(0, (--p).Index);
            Assert.Equal(0, p.Index);
        }

        [Fact]
        public void Equality()
        {
            int[] source = Enumerable.Range(1, 10).ToArray();

            var p1 = new Pointer<int>(source);
            Assert.True(p1 == source);
            Assert.False(p1 != source);
            Assert.True(source == p1);
            Assert.False(source != p1);

            var p2 = new Pointer<int>(source, 6);
            Assert.False(p2 == source);
            Assert.True(p2 != source);

            var p3 = new Pointer<int>(null, 0);
            Assert.False(p3 == source);
            Assert.True(p3 != source);

            Assert.True(p1 == new Pointer<int>(source, 0));
            Assert.False(p1 != new Pointer<int>(source, 0));
            Assert.False(p1 == new Pointer<int>(source, 6));
            Assert.True(p1 != new Pointer<int>(source, 6));
            Assert.False(p2 == new Pointer<int>(source, 0));
            Assert.True(p2 != new Pointer<int>(source, 0));
            Assert.True(p2 == new Pointer<int>(source, 6));
            Assert.False(p2 != new Pointer<int>(source, 6));
            Assert.False(p3 == new Pointer<int>(source, 0));
            Assert.True(p3 != new Pointer<int>(source, 0));
            Assert.False(p3 == new Pointer<int>(source, 6));
            Assert.True(p3 != new Pointer<int>(source, 6));

            Assert.False(p1 == null);
            Assert.True(p1 != null);
            Assert.True(p3 == null);
            Assert.False(p3 != null);
        }

    }
}
