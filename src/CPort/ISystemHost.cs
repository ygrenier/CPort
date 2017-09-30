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
        Stream OpenFile(string filename, CFileMode fMode);
    }
}
