using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CPort
{
    /// <summary>
    /// System host representation
    /// </summary>
    public interface ISystemHost
    {
        /// <summary>
        /// Open a file
        /// </summary>
        Tuple<Stream, Encoding> OpenFile(string filename, CFileMode fMode);

        /// <summary>
        /// Current default encoding for files
        /// </summary>
        Encoding DefaultFileEncoding { get; }
    }
}
