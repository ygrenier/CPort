using System;
using System.Collections.Generic;
using System.Text;

namespace CPort
{
    /// <summary>
    /// Represents a NULL pointer
    /// </summary>
    public struct NullPointer
    {
        /// <summary>
        /// Null constant
        /// </summary>
        public static readonly NullPointer Null = new NullPointer();

        /// <summary>
        /// Check equality
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is NullPointer)
                return true;
            if (obj is IPointer ptr)
                return ptr.IsNull == IsNull;
            return base.Equals(obj);
        }

        /// <summary>
        /// Hashcode
        /// </summary>
        public override int GetHashCode()
        {
            return 0;
        }

        #region Equality
        /// <summary>
        /// Test the equality of the pointer with a null pointer
        /// </summary>
        public static bool operator ==(NullPointer npointer, NullPointer pointer)
        {
            return npointer.IsNull == pointer.IsNull;
        }

        /// <summary>
        /// Test the non equality of the pointer with a null pointer
        /// </summary>
        public static bool operator !=(NullPointer npointer, NullPointer pointer)
        {
            return npointer.IsNull != pointer.IsNull;
        }

        #endregion

        /// <summary>
        /// Indicates if the pointer is null
        /// </summary>
        public bool IsNull => true;
    }
}
