using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPort
{

    /// <summary>
    /// C pointer simulator
    /// </summary>
    public struct Pointer<T> : IEnumerable<T>
    {

        #region Ctors
        /// <summary>
        /// Create a new pointer
        /// </summary>
        public Pointer(IList<T> source) : this(source, 0)
        { }

        /// <summary>
        /// Create a new pointer with a source and an index
        /// </summary>
        public Pointer(IList<T> source, int index)
        {
            Source = source;
            Index = Math.Max(0, source != null ? index : 0);
        }

        /// <summary>
        /// Create a new pointer based on a new buffer of a <paramref name="size"/> elements
        /// </summary>
        public Pointer(int size)
        {
            Source = new T[size];
            Index = 0;
        }
        #endregion

        #region Enumerable
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (Source ?? Enumerable.Empty<T>()).Skip(Index).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
        #endregion

        #region Overrides

        /// <summary>
        /// HashCode
        /// </summary>
        public override int GetHashCode()
        {
            return (Source?.GetHashCode() ?? 0) ^ Index.GetHashCode();
        }

        /// <summary>
        /// Determine if <paramref name="obj"/> is equals with this pointer
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is IList<T>)
            {
                return Source == obj && Index == 0;
            }
            else if (obj is Pointer<T> pobj)
            {
                return Source == pobj.Source && Index == pobj.Index;
            }
            return base.Equals(obj);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Indicates if the pointer is null
        /// </summary>
        public bool IsNull => Source == null;

        /// <summary>
        /// Source
        /// </summary>
        public IList<T> Source { get; private set; }

        /// <summary>
        /// Current index in the source of the pointer
        /// </summary>
        public int Index { get; private set; }

        #endregion
    }

}
