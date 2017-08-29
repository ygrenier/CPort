using System;
using System.Collections.Generic;
using System.Text;

namespace CPort
{
    /// <summary>
    /// Exception raised when get/set value on a pointer is out of range
    /// </summary>
    public class PointerOutOfRangeException : Exception
    {
        /// <summary>
        /// New exception
        /// </summary>
        public PointerOutOfRangeException() : this("This pointer index value is out of range of the source.")
        {
        }

        /// <summary>
        /// New exception with a message
        /// </summary>
        public PointerOutOfRangeException(string message) : base(message)
        {
        }

        /// <summary>
        /// New exception with an index
        /// </summary>
        public PointerOutOfRangeException(int index) : this($"This pointer index ({index}) value is out of range of the source.")
        {
            Index = index;
        }

        /// <summary>
        /// New exception with a message and an idnex
        /// </summary>
        public PointerOutOfRangeException(int index, string message) : base(message)
        {
            Index = index;
        }

        /// <summary>
        /// Index
        /// </summary>
        public int? Index { get; private set; } = null;

    }
}
