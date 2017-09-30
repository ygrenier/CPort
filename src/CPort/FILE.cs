﻿using System;
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
        public FILE(Stream source, CFileMode mode)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Mode = mode;
        }

        /// <summary>
        /// Dispose the source stream
        /// </summary>
        public void Dispose()
        {
            Source?.Dispose();
            Source = null;
            Mode = null;
        }

        /// <summary>
        /// Reopen the file
        /// </summary>
        public void Reopen(Stream source, CFileMode mode)
        {
            if(source==null) throw new ArgumentNullException(nameof(source)); 
            if (Source != null) Source.Dispose();
            Source = source;
            Mode = mode;
        }

        /// <summary>
        /// Source of file
        /// </summary>
        public Stream Source { get; private set; }

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
