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
        public virtual Stream OpenFile(string filename, CFileMode fMode, out Encoding encoding)
        {
            encoding = null;
            return null;
        }

        /// <summary>
        /// Current default encoding for files
        /// </summary>
        public Encoding DefaultFileEncoding { get; set; }

    }
}
