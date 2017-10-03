using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CPort
{
    /// <summary>
    /// Default system host implementation
    /// </summary>
    public class DefaultSystemHost : ISystemHost
    {
        /// <summary>
        /// Create a new host
        /// </summary>
        public DefaultSystemHost()
        {
            DefaultFileEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// Open a file
        /// </summary>
        public virtual Tuple<Stream, Encoding> OpenFile(string filename, CFileMode fMode)
            => null;

        /// <summary>
        /// Current default encoding for files
        /// </summary>
        public Encoding DefaultFileEncoding { get; set; }
    }
}
