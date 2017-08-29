using System;
using System.Collections.Generic;
using System.Text;

namespace CPort
{
    /// <summary>
    /// Exception raised when a pointer is null
    /// </summary>
    public class PointerNullException : Exception
    {
        /// <summary>
        /// Create a new exception
        /// </summary>
        public PointerNullException() : base("This pointer is null.") { }

        /// <summary>
        /// Create a new exception with a message
        /// </summary>
        public PointerNullException(string message) : base(message) { }

    }
}
