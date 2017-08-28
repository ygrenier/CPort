using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CPort.Tests.Extensions
{
    public class TypeExtensionsTest
    {

        [Fact]
        public void GetTypeCode()
        {
            Type tp = null;
            Assert.Equal(TypeCode.Empty, tp.GetTypeCode());
            Assert.Equal(TypeCode.Boolean, typeof(bool).GetTypeCode());
            Assert.Equal(TypeCode.Byte, typeof(byte).GetTypeCode());
            Assert.Equal(TypeCode.Char, typeof(char).GetTypeCode());
            Assert.Equal(TypeCode.UInt16, typeof(ushort).GetTypeCode());
            Assert.Equal(TypeCode.UInt32, typeof(uint).GetTypeCode());
            Assert.Equal(TypeCode.UInt64, typeof(ulong).GetTypeCode());
            Assert.Equal(TypeCode.SByte, typeof(sbyte).GetTypeCode());
            Assert.Equal(TypeCode.Int16, typeof(short).GetTypeCode());
            Assert.Equal(TypeCode.Int32, typeof(int).GetTypeCode());
            Assert.Equal(TypeCode.Int64, typeof(long).GetTypeCode());
            Assert.Equal(TypeCode.String, typeof(string).GetTypeCode());
            Assert.Equal(TypeCode.Single, typeof(float).GetTypeCode());
            Assert.Equal(TypeCode.Double, typeof(double).GetTypeCode());
            Assert.Equal(TypeCode.DateTime, typeof(DateTime).GetTypeCode());
            Assert.Equal(TypeCode.Decimal, typeof(Decimal).GetTypeCode());
            Assert.Equal(TypeCode.Object, typeof(TypeExtensionsTest).GetTypeCode());
            Assert.Equal(TypeCode.Object, typeof(TypeCode).GetTypeCode());
        }

    }
}
