using System;
using System.Collections.Generic;
using System.IO;
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
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
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
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            Mode = mode;
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
