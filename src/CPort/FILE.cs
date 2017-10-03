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

        /// <summary>
        /// Open a file
        /// </summary>
        public FILE(Stream source, CFileMode mode, Encoding encoding)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Encoding = mode.HasFlag(CFileMode.Binary) ? encoding : encoding ?? throw new ArgumentNullException(nameof(encoding));
            Mode = mode;
        }

        /// <summary>
        /// Dispose the source stream
        /// </summary>
        public void Dispose()
        {
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
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Encoding = mode.HasFlag(CFileMode.Binary) ? encoding : encoding ?? throw new ArgumentNullException(nameof(encoding));
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

        bool CanWrite() => Source?.CanWrite == true
            && (Mode.Value.HasFlag(CFileMode.Update) || Mode.Value.HasFlag(CFileMode.Write) || Mode.Value.HasFlag(CFileMode.Append));

        /// <summary>
        /// Write a string in the file
        /// </summary>
        public int Write(string value)
        {
            if (!CanWrite()) return -1;
            if (string.IsNullOrWhiteSpace(value)) return 0;
            var b = EncodeString(value);
            Source.Write(b, 0, b.Length);
            return b.Length;
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
