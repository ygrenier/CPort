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
        /// Open a file
        /// </summary>
        public virtual Stream OpenFile(string filename, CFileMode fMode)
            => null;

    }
}
