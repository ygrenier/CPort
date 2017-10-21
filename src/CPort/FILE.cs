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
        Decoder _decoder = null;
        byte[] _decodeBuffer = new byte[12];
        char[] _charsDecoded = new char[12];
        Stack<char> _ungetBuffer = new Stack<char>();

        /// <summary>
        /// Open a file
        /// </summary>
        public FILE(Stream source, CFileMode mode, Encoding encoding)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Encoding = mode.HasFlag(CFileMode.Binary) ? encoding : encoding ?? throw new ArgumentNullException(nameof(encoding));
            _decoder = Encoding?.GetDecoder();
            Mode = mode;
        }

        /// <summary>
        /// Dispose the source stream
        /// </summary>
        public void Dispose()
        {
            Source?.Dispose();
            Source = null;
            _decoder = null;
            Encoding = null;
            Mode = null;
        }

        /// <summary>
        /// Reopen the file
        /// </summary>
        public void Reopen(Stream source, CFileMode mode, Encoding encoding)
        {
            if (Source != null) Dispose();
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Encoding = mode.HasFlag(CFileMode.Binary) ? encoding : encoding ?? throw new ArgumentNullException(nameof(encoding));
            _decoder = Encoding?.GetDecoder();
            Mode = mode;
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
            int c, byteIndex = 0, byteCount = 1;
            _decoder.Convert(_decodeBuffer, 0, 0, _charsDecoded, 0, _charsDecoded.Length, true, out int bytesUsed, out int charsUsed, out bool completed);
            while ((c = Source.ReadByte()) != -1)
            {
                _decodeBuffer[byteIndex] = (byte)c;
                _decoder.Convert(_decodeBuffer, byteIndex, byteCount
                    , _charsDecoded, 0, _charsDecoded.Length, false
                    , out bytesUsed, out charsUsed, out completed);
                if (charsUsed > 0) return _charsDecoded[0];
                byteIndex++;
            }
            return null;
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
            Source.Write(buffer, offset, count);
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
