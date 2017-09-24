using static CPort.C;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CPort.Tests
{
    public class NullPointerTest
    {
        [Fact]
        public void Create()
        {
            Assert.True(NullPointer.Null.IsNull);
            var p = new NullPointer();
            Assert.True(p.IsNull);
        }

        [Fact]
        public void HashCode()
        {
            Assert.Equal(0, NULL.GetHashCode());
        }

        [Fact]
        public void Equality()
        {
            var pn = new Pointer<int>();
            var pnn = new Pointer<int>(new int[] { 1, 2, 3, 4, 5 });
            var np = new NullPointer();

            Assert.True(np.Equals(pn));
            Assert.False(np.Equals(pnn));
            Assert.True(np.Equals(np));
            Assert.False(np.Equals(123));

            Assert.True(np == pn);
            Assert.False(np == pnn);
            Assert.True(np == NULL);

            Assert.False(np != pn);
            Assert.True(np != pnn);
            Assert.False(np != NULL);

            Assert.True(pn == np);
            Assert.False(pnn == np);
            Assert.True(NULL == np);

            Assert.False(pn != np);
            Assert.True(pnn != np);
            Assert.False(NULL != np);
        }

    }
}
