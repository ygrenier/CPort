using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CPort
{

    /// <summary>
    /// C file object
    /// </summary>
    public sealed class FILE : IDisposable
    {
        BinaryReader _breader;
        BinaryWriter _bwriter;
        Stack<char> _ungetBuffer = new Stack<char>();

        /// <summary>
        /// Open a file
        /// </summary>
        public FILE(Stream source, CFileMode mode, Encoding encoding)
        {
            Init(source, mode, encoding);
        }

        private void Init(Stream source, CFileMode mode, Encoding encoding)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Encoding = mode.HasFlag(CFileMode.Binary) ? encoding : encoding ?? throw new ArgumentNullException(nameof(encoding));
            if (mode.HasFlag(CFileMode.Read) | mode.HasFlag(CFileMode.Update))
                _breader = new BinaryReader(Source, Encoding ?? Encoding.UTF8);
            if (mode.HasFlag(CFileMode.Write) | mode.HasFlag(CFileMode.Append) | mode.HasFlag(CFileMode.Update))
                _bwriter = new BinaryWriter(Source, Encoding ?? Encoding.UTF8);
            Mode = mode;
        }

        /// <summary>
        /// Dispose the source stream
        /// </summary>
        public void Dispose()
        {
            _breader?.Dispose();
            _breader = null;
            _bwriter?.Dispose();
            _bwriter = null;
            Source?.Dispose();
            Source = null;
            Encoding = null;
            Mode = null;
        }

        /// <summary>
        /// Reopen the file
        /// </summary>
        public void Reopen(Stream source, CFileMode mode, Encoding encoding)
        {
            if (Source != null) Dispose();
            Init(source, mode, encoding);
        }

        /// <summary>
        /// Encode a string
        /// </summary>
        byte[] EncodeString(string value)
        {
            if (value != null && !Mode.Value.HasFlag(CFileMode.Binary))
                value = value.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
            return Encoding != null ? Encoding.GetBytes(value) : value.Cast<char>().Select(c => (byte)c).ToArray();
        }

        /// <summary>
        /// Read by decoding the next char in the source
        /// </summary>
        char? ReadNextChar()
        {
            if (_ungetBuffer.Count > 0) return _ungetBuffer.Pop();
            int c = _breader.Read();
            return c == -1 ? (char?)null : (char)c;
        }

        /// <summary>
        /// Read the next char by decoding the new line chars
        /// </summary>
        /// <returns></returns>
        char? ReadChar()
        {
            char? res = ReadNextChar();
            if (!res.HasValue) return res;
            char c = res.Value;
            if (c == 13 && !Mode.Value.HasFlag(CFileMode.Binary))
            {
                res = ReadNextChar();
                if (res.HasValue)
                {
                    char cc = res.Value;
                    if (cc != 10)
                        _ungetBuffer.Push(cc);
                }
                c = (char)10;
            }
            return c;
        }

        bool CanWrite() => Source?.CanWrite == true
            && (Mode.Value.HasFlag(CFileMode.Update) || Mode.Value.HasFlag(CFileMode.Write) || Mode.Value.HasFlag(CFileMode.Append));

        bool CanRead() => Source?.CanRead == true
            && (Mode.Value.HasFlag(CFileMode.Update) || Mode.Value.HasFlag(CFileMode.Read));

        /// <summary>
        /// Write a string in the file
        /// </summary>
        public int Write(string value)
        {
            if (!CanWrite()) return C.EOF;
            if (string.IsNullOrEmpty(value)) return 0;
            var b = EncodeString(value);
            return Write(b, 0, b.Length);
        }

        /// <summary>
        /// Write a byte buffer in the file
        /// </summary>
        public int Write(byte[] buffer, int offset, int count)
        {
            if (!CanWrite()) return C.EOF;
            _bwriter.Write(buffer, offset, count);
            return count;
        }

        /// <summary>
        /// Write a char buffer int the file
        /// </summary>
        public int Write(char[] buffer, int offset, int count)
        {
            if (!CanWrite()) return C.EOF;
            _bwriter.Write(buffer, offset, count);
            return count;
        }

        /// <summary>
        /// Write a sbyte buffer int the file
        /// </summary>
        public int Write(sbyte[] buffer, int offset, int count)
        {
            if (!CanWrite()) return C.EOF;
            for (int i = 0; i < count; i++)
                _bwriter.Write(buffer[offset + i]);
            return count;
        }

        /// <summary>
        /// Write a ushort buffer int the file
        /// </summary>
        public int Write(ushort[] buffer, int offset, int count)
        {
            if (!CanWrite()) return C.EOF;
            for (int i = 0; i < count; i++)
                _bwriter.Write(buffer[offset + i]);
            return count;
        }

        /// <summary>
        /// Write a short buffer int the file
        /// </summary>
        public int Write(short[] buffer, int offset, int count)
        {
            if (!CanWrite()) return C.EOF;
            for (int i = 0; i < count; i++)
                _bwriter.Write(buffer[offset + i]);
            return count;
        }

        /// <summary>
        /// Write a uint buffer int the file
        /// </summary>
        public int Write(uint[] buffer, int offset, int count)
        {
            if (!CanWrite()) return C.EOF;
            for (int i = 0; i < count; i++)
                _bwriter.Write(buffer[offset + i]);
            return count;
        }

        /// <summary>
        /// Write a int buffer int the file
        /// </summary>
        public int Write(int[] buffer, int offset, int count)
        {
            if (!CanWrite()) return C.EOF;
            for (int i = 0; i < count; i++)
                _bwriter.Write(buffer[offset + i]);
            return count;
        }

        /// <summary>
        /// Write a ulong buffer int the file
        /// </summary>
        public int Write(ulong[] buffer, int offset, int count)
        {
            if (!CanWrite()) return C.EOF;
            for (int i = 0; i < count; i++)
                _bwriter.Write(buffer[offset + i]);
            return count;
        }

        /// <summary>
        /// Write a long buffer int the file
        /// </summary>
        public int Write(long[] buffer, int offset, int count)
        {
            if (!CanWrite()) return C.EOF;
            for (int i = 0; i < count; i++)
                _bwriter.Write(buffer[offset + i]);
            return count;
        }

        /// <summary>
        /// Write a float buffer int the file
        /// </summary>
        public int Write(float[] buffer, int offset, int count)
        {
            if (!CanWrite()) return C.EOF;
            for (int i = 0; i < count; i++)
                _bwriter.Write(buffer[offset + i]);
            return count;
        }

        /// <summary>
        /// Write a double buffer int the file
        /// </summary>
        public int Write(double[] buffer, int offset, int count)
        {
            if (!CanWrite()) return C.EOF;
            for (int i = 0; i < count; i++)
                _bwriter.Write(buffer[offset + i]);
            return count;
        }

        /// <summary>
        /// Read the next character
        /// </summary>
        /// <returns>The next character or <see cref="C.EOF"/> </returns>
        public int Read()
        {
            if (!CanRead()) return C.EOF;
            var c = ReadChar();
            if (!c.HasValue) return C.EOF;
            return c.Value;
        }

        /// <summary>
        /// Read a string
        /// </summary>
        public string ReadString(int count)
        {
            var buffer = new char[count];
            var r = Read(buffer, 0, count);
            if (r < 0) return null;
            return new string(buffer, 0, r);
        }

        /// <summary>
        /// Read a byte buffer in the file
        /// </summary>
        public int Read(byte[] buffer, int offset, int count)
        {
            if (!CanRead()) return C.EOF;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    buffer[offset + i] = _breader.ReadByte();
                }
                catch { return i; }
            }
            return count;
        }

        /// <summary>
        /// Read a char buffer int the file
        /// </summary>
        public int Read(char[] buffer, int offset, int count)
        {
            if (!CanRead()) return C.EOF;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    buffer[offset + i] = _breader.ReadChar();
                }
                catch { return i; }
            }
            return count;
        }

        /// <summary>
        /// Read a sbyte buffer int the file
        /// </summary>
        public int Read(sbyte[] buffer, int offset, int count)
        {
            if (!CanRead()) return C.EOF;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    buffer[offset + i] = _breader.ReadSByte();
                }
                catch { return i; }
            }
            return count;
        }

        /// <summary>
        /// Read a ushort buffer int the file
        /// </summary>
        public int Read(ushort[] buffer, int offset, int count)
        {
            if (!CanRead()) return C.EOF;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    buffer[offset + i] = _breader.ReadUInt16();
                }
                catch { return i; }
            }
            return count;
        }

        /// <summary>
        /// Read a short buffer int the file
        /// </summary>
        public int Read(short[] buffer, int offset, int count)
        {
            if (!CanRead()) return C.EOF;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    buffer[offset + i] = _breader.ReadInt16();
                }
                catch { return i; }
            }
            return count;
        }

        /// <summary>
        /// Read a uint buffer int the file
        /// </summary>
        public int Read(uint[] buffer, int offset, int count)
        {
            if (!CanRead()) return C.EOF;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    buffer[offset + i] = _breader.ReadUInt32();
                }
                catch { return i; }
            }
            return count;
        }

        /// <summary>
        /// Read a int buffer int the file
        /// </summary>
        public int Read(int[] buffer, int offset, int count)
        {
            if (!CanRead()) return C.EOF;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    buffer[offset + i] = _breader.ReadInt32();
                }
                catch { return i; }
            }
            return count;
        }

        /// <summary>
        /// Read a ulong buffer int the file
        /// </summary>
        public int Read(ulong[] buffer, int offset, int count)
        {
            if (!CanRead()) return C.EOF;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    buffer[offset + i] = _breader.ReadUInt64();
                }
                catch { return i; }
            }
            return count;
        }

        /// <summary>
        /// Read a long buffer int the file
        /// </summary>
        public int Read(long[] buffer, int offset, int count)
        {
            if (!CanRead()) return C.EOF;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    buffer[offset + i] = _breader.ReadInt64();
                }
                catch { return i; }
            }
            return count;
        }

        /// <summary>
        /// Read a float buffer int the file
        /// </summary>
        public int Read(float[] buffer, int offset, int count)
        {
            if (!CanRead()) return C.EOF;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    buffer[offset + i] = _breader.ReadSingle();
                }
                catch { return i; }
            }
            return count;
        }

        /// <summary>
        /// Read a double buffer int the file
        /// </summary>
        public int Read(double[] buffer, int offset, int count)
        {
            if (!CanRead()) return C.EOF;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    buffer[offset + i] = _breader.ReadDouble();
                }
                catch { return i; }
            }
            return count;
        }

        /// <summary>
        /// Place a char in the unread buffer
        /// </summary>
        public bool UnreadChar(char c)
        {
            if (!CanRead()) return false;
            _ungetBuffer.Push(c);
            return true;
        }

        /// <summary>
        /// Source of file
        /// </summary>
        public Stream Source { get; private set; }

        /// <summary>
        /// Encoding
        /// </summary>
        public Encoding Encoding { get; private set; }

        /// <summary>
        /// File mode
        /// </summary>
        public CFileMode? Mode { get; private set; }

        /// <summary>
        /// Position
        /// </summary>
        public long Position => Source?.Position ?? -1L;
    }

    /// <summary>
    /// File mode
    /// </summary>
    [Flags]
    public enum CFileMode
    {
        Read = 1,
        Write = 2,
        Append = 3,
        Update = 8,
        Binary = 16
    }

}
