using static CPort.C;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CPort.Tests
{
    public class CTypeTest
    {
        [Fact]
        public void Cisalnum()
        {
            Assert.True(isalnum('0'));
            Assert.True(isalnum('9'));
            Assert.True(isalnum('a'));
            Assert.False(isalnum('.'));
            Assert.False(isalnum('\t'));
            Assert.False(isalnum(' '));
        }

        [Fact]
        public void Cisalpha()
        {
            Assert.False(isalpha('0'));
            Assert.False(isalpha('9'));
            Assert.True(isalpha('a'));
            Assert.False(isalpha('.'));
            Assert.False(isalpha('\t'));
            Assert.False(isalpha(' '));
        }

        [Fact]
        public void Ciscntrl()
        {
            Assert.False(iscntrl('0'));
            Assert.False(iscntrl('9'));
            Assert.False(iscntrl('a'));
            Assert.False(iscntrl('.'));
            Assert.True(iscntrl('\t'));
            Assert.False(iscntrl(' '));
        }

        [Fact]
        public void Cisdigit()
        {
            Assert.True(isdigit('0'));
            Assert.True(isdigit('9'));
            Assert.False(isdigit('a'));
            Assert.False(isdigit('.'));
            Assert.False(isdigit('\t'));
            Assert.False(isdigit(' '));
        }

        [Fact]
        public void Cisgraph()
        {
            Assert.True(isgraph('0'));
            Assert.True(isgraph('9'));
            Assert.True(isgraph('a'));
            Assert.True(isgraph('.'));
            Assert.False(isgraph('\t'));
            Assert.False(isgraph(' '));
        }

        [Fact]
        public void Cislower()
        {
            Assert.False(islower('0'));
            Assert.False(islower('9'));
            Assert.True(islower('a'));
            Assert.False(islower('B'));
            Assert.False(islower('.'));
            Assert.False(islower('\t'));
            Assert.False(islower(' '));
        }

        [Fact]
        public void Cisprint()
        {
            Assert.True(isprint('0'));
            Assert.True(isprint('9'));
            Assert.True(isprint('a'));
            Assert.True(isprint('.'));
            Assert.False(isprint('\t'));
            Assert.True(isprint(' '));
        }

        [Fact]
        public void Cispunct()
        {
            Assert.False(ispunct('0'));
            Assert.False(ispunct('9'));
            Assert.False(ispunct('a'));
            Assert.False(ispunct('B'));
            Assert.True(ispunct('.'));
            Assert.False(ispunct('\t'));
            Assert.False(ispunct(' '));
        }

        [Fact]
        public void Cisspace()
        {
            Assert.False(isspace('0'));
            Assert.False(isspace('9'));
            Assert.False(isspace('a'));
            Assert.False(isspace('B'));
            Assert.False(isspace('.'));
            Assert.True(isspace('\t'));
            Assert.True(isspace(' '));
        }

        [Fact]
        public void Cisupper()
        {
            Assert.False(isupper('0'));
            Assert.False(isupper('9'));
            Assert.False(isupper('a'));
            Assert.True(isupper('B'));
            Assert.False(isupper('.'));
            Assert.False(isupper('\t'));
            Assert.False(isupper(' '));
        }

        [Fact]
        public void Cisxdigit()
        {
            Assert.True(isxdigit('0'));
            Assert.True(isxdigit('9'));
            Assert.True(isxdigit('a'));
            Assert.True(isxdigit('B'));
            Assert.False(isxdigit('.'));
            Assert.False(isxdigit('\t'));
            Assert.False(isxdigit(' '));
        }

        [Fact]
        public void Ctolower()
        {
            Assert.Equal('0', tolower('0'));
            Assert.Equal('9', tolower('9'));
            Assert.Equal('a', tolower('a'));
            Assert.Equal('b', tolower('B'));
            Assert.Equal('.', tolower('.'));
            Assert.Equal('\t', tolower('\t'));
            Assert.Equal(' ', tolower(' '));
        }

        [Fact]
        public void Ctoupper()
        {
            Assert.Equal('0', toupper('0'));
            Assert.Equal('9', toupper('9'));
            Assert.Equal('A', toupper('a'));
            Assert.Equal('B', toupper('B'));
            Assert.Equal('.', toupper('.'));
            Assert.Equal('\t', toupper('\t'));
            Assert.Equal(' ', toupper(' '));
        }

    }
}
