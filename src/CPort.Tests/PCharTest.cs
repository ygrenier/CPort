using static CPort.C;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CPort.Tests
{
    public class PCharTest
    {

        [Fact]
        public void Create()
        {
            PChar p = new PChar();
            Assert.True(p.IsNull);
            Assert.Null(p.Source);
            Assert.Equal(0, p.Index);

            p = new PChar(10);
            Assert.False(p.IsNull);
            Assert.NotNull(p.Source);
            Assert.Equal(10, p.Source.Count);
            Assert.Equal(0, p.Index);

            char[] source = new char[] { '1', '2', '3' };
            p = new PChar(source);
            Assert.False(p.IsNull);
            Assert.NotNull(p.Source);
            Assert.Same(source, p.Source);
            Assert.Equal(0, p.Index);

            p = new PChar(source, 4);
            Assert.False(p.IsNull);
            Assert.NotNull(p.Source);
            Assert.Same(source, p.Source);
            Assert.Equal(4, p.Index);

            p = new PChar(source, -4);
            Assert.False(p.IsNull);
            Assert.NotNull(p.Source);
            Assert.Same(source, p.Source);
            Assert.Equal(0, p.Index);

            p = new PChar((char[])null);
            Assert.True(p.IsNull);
            Assert.Null(p.Source);
            Assert.Equal(0, p.Index);

            p = new PChar(null, 4);
            Assert.True(p.IsNull);
            Assert.Null(p.Source);
            Assert.Equal(0, p.Index);

            p = new PChar("abcd");
            Assert.False(p.IsNull);
            Assert.NotNull(p.Source);
            Assert.Equal(5, p.Source.Count);
            Assert.Equal(new char[] { 'a', 'b', 'c', 'd', '\0' }, p.Source);
            Assert.Equal(0, p.Index);

            p = new PChar((string)null);
            Assert.True(p.IsNull);
            Assert.Null(p.Source);
            Assert.Equal(0, p.Index);
        }

        [Fact]
        public void AsEnumerable()
        {
            char[] source = Enumerable.Range(0, 10).Select(i => (char)('a' + i)).ToArray();

            var p = new PChar(source);
            Assert.Equal(source, p.ToArray());

            p = new PChar(source, 6);
            Assert.Equal(new char[] { 'g', 'h', 'i', 'j' }, p.ToArray());

            p = new PChar(source, 50);
            Assert.Equal(new char[] { }, p.ToArray());

            p = new PChar(source, -50);
            Assert.Equal(source, p.ToArray());

            IEnumerable penum = p;
            Assert.Equal(source, penum.OfType<char>().ToArray());

            p = new PChar(null, 0);
            Assert.Equal(new char[] { }, p.ToArray());
        }

        [Fact]
        public void TestGetHashCode()
        {
            char[] source = Enumerable.Range(0, 10).Select(i => (char)('a' + i)).ToArray();

            var p = new PChar(source);
            Assert.Equal(source.GetHashCode() ^ 0, p.GetHashCode());

            p = new PChar(source, 6);
            Assert.Equal(source.GetHashCode() ^ 6, p.GetHashCode());

            p = new PChar(source, 50);
            Assert.Equal(source.GetHashCode() ^ 50, p.GetHashCode());

            p = new PChar(source, -50);
            Assert.Equal(source.GetHashCode() ^ 0, p.GetHashCode());

            p = new PChar(null, 0);
            Assert.Equal(0, p.GetHashCode());
        }

        [Fact]
        public void TestEquals()
        {
            char[] source = Enumerable.Range(0, 10).Select(i => (char)('a' + i)).ToArray();

            var p1 = new PChar(source);
            Assert.True(p1.Equals(source));

            var p2 = new PChar(source, 6);
            Assert.False(p2.Equals(source));

            var p3 = new PChar(null, 0);
            Assert.False(p3.Equals(source));

            Assert.True(p1.Equals(new PChar(source, 0)));
            Assert.False(p1.Equals(new PChar(source, 6)));
            Assert.False(p2.Equals(new PChar(source, 0)));
            Assert.True(p2.Equals(new PChar(source, 6)));
            Assert.False(p3.Equals(new PChar(source, 0)));
            Assert.False(p3.Equals(new PChar(source, 6)));

            Assert.True(p1.Equals(new Pointer<char>(source, 0)));
            Assert.False(p1.Equals(new Pointer<char>(source, 6)));
            Assert.False(p2.Equals(new Pointer<char>(source, 0)));
            Assert.True(p2.Equals(new Pointer<char>(source, 6)));
            Assert.False(p3.Equals(new Pointer<char>(source, 0)));
            Assert.False(p3.Equals(new Pointer<char>(source, 6)));

            Assert.False(p1.Equals(null));
            Assert.False(p1.Equals(123));
        }

        [Fact]
        public void GetString()
        {
            var pointer = new PChar(new char[] { 'T', 'e', 's', 't', '\0', 'T', 'e', 's', 't', '\0' });
            Assert.Equal("Test", pointer.GetString());

            Assert.Equal("st", (pointer + 2).GetString());
            Assert.Equal("", (pointer + 4).GetString());
            Assert.Equal("est", (pointer + 6).GetString());
            Assert.Equal("t", (pointer + 8).GetString());
            Assert.Equal("", (pointer + 10).GetString());

            pointer = (char[])null;
            Assert.Equal(null, pointer.GetString());
            Assert.Equal(null, (pointer + 2).GetString());

        }

        [Fact]
        public void Casts()
        {
            char[] array = Enumerable.Range(0, 10).Select(i => (char)('a' + i)).ToArray();
            List<char> list = new List<char>(array);

            PChar p = array;
            Assert.Same(array, p.Source);
            Assert.Equal(0, p.Index);

            p = list;
            Assert.Same(list, p.Source);
            Assert.Equal(0, p.Index);

            char[] narray = (char[])p;
            Assert.Equal(narray, array);

            List<char> nlist = (List<char>)p;
            Assert.Equal(nlist, list);

            p = new PChar(array, 5);
            narray = (char[])p;
            Assert.Equal(narray, array.Skip(5));
            nlist = (List<char>)p;
            Assert.Equal(nlist, list.Skip(5));

            p = new PChar();
            narray = (char[])p;
            Assert.Null(narray);
            nlist = (List<char>)p;
            Assert.Null(nlist);

            // Cast from Pointer<char>
            p = array;
            Pointer<char> poc = p;
            Assert.Same(array, poc.Source);
            Assert.Equal(0, poc.Index);
            p = p + 3;
            poc = p;
            Assert.Same(array, poc.Source);
            Assert.Equal(3, poc.Index);
            poc --;
            p = poc;
            Assert.Same(array, p.Source);
            Assert.Equal(2, p.Index);

            // Cast of string
            p = "test";
            Assert.Equal(new char[] { 't', 'e', 's', 't', '\0' }, p.Source);
            Assert.Equal(0, p.Index);
            p[2] = 'Z';
            string s = (string)p;
            Assert.Equal("teZt", s);

            // Cast from NullPointer
            p = array;
            Assert.False(p.IsNull);
            p = NULL;
            Assert.True(p.IsNull);
        }

        [Fact]
        public void ValueAccess()
        {
            char[] source = Enumerable.Range(0, 10).Select(i => (char)('a' + i)).ToArray();

            var p = new PChar(source, 5);
            Assert.Equal('f', p);
            Assert.Equal('f', p[0]);
            Assert.Equal('f', p.Value);
            Assert.Throws<PointerOutOfRangeException>(() => Assert.Equal('f', p[123]));
            Assert.True(p.TryGetValue(0, out char actual));
            Assert.Equal('f', actual);
            Assert.False(p.TryGetValue(123, out actual));
            Assert.Equal(0, actual);

            p.Value = 'm';
            p[2] = 'n';
            Assert.Throws<PointerOutOfRangeException>(() => p[123] = 'm');
            Assert.True(p.TrySetValue('s', 0));
            Assert.False(p.TrySetValue('z', 123));

            Assert.Equal(new char[] { 'a', 'b', 'c', 'd', 'e', 's', 'g', 'n', 'i', 'j' }, source);

            p = new PChar(null, 5);
            Assert.Throws<PointerNullException>(() => actual = p);
            Assert.Throws<PointerNullException>(() => actual = p.Value);
            Assert.Throws<PointerNullException>(() => actual = p[0]);
            Assert.Throws<PointerNullException>(() => p.Value = 'm');
            Assert.Throws<PointerNullException>(() => p[0] = 'n');
            Assert.False(p.TryGetValue(0, out actual));
            Assert.False(p.TrySetValue('s', 0));

        }

        [Fact]
        public void PointerAddSub()
        {
            char[] source = Enumerable.Range(0, 10).Select(i => (char)('a' + i)).ToArray();

            var p1 = new PChar(source);
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
            char[] source = Enumerable.Range(0, 10).Select(i => (char)('a' + i)).ToArray();

            var p = new PChar(source);
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
            char[] source = Enumerable.Range(0, 10).Select(i => (char)('a' + i)).ToArray();

            var p1 = new PChar(source);
            Assert.True(p1 == source);
            Assert.False(p1 != source);
            Assert.True(source == p1);
            Assert.False(source != p1);

            var p2 = new PChar(source, 6);
            Assert.False(p2 == source);
            Assert.True(p2 != source);

            var p3 = new PChar(null, 0);
            Assert.False(p3 == source);
            Assert.True(p3 != source);

            Assert.True(p1 == new PChar(source, 0));
            Assert.False(p1 != new PChar(source, 0));
            Assert.False(p1 == new PChar(source, 6));
            Assert.True(p1 != new PChar(source, 6));
            Assert.False(p2 == new PChar(source, 0));
            Assert.True(p2 != new PChar(source, 0));
            Assert.True(p2 == new PChar(source, 6));
            Assert.False(p2 != new PChar(source, 6));
            Assert.False(p3 == new PChar(source, 0));
            Assert.True(p3 != new PChar(source, 0));
            Assert.False(p3 == new PChar(source, 6));
            Assert.True(p3 != new PChar(source, 6));

            Assert.False(p1 == null);
            Assert.True(p1 != null);
            Assert.True(p3 == null);
            Assert.False(p3 != null);
        }

    }
}
