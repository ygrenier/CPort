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

        #region Casts

        /// <summary>
        /// Implicit conversion of a list to a pointer
        /// </summary>
        public static implicit operator Pointer<T>(List<T> source)
        {
            return new Pointer<T>(source);
        }

        /// <summary>
        /// Implicit conversion of an array to a pointer
        /// </summary>
        public static implicit operator Pointer<T>(T[] source)
        {
            return new Pointer<T>(source);
        }

        /// <summary>
        /// Explicit conversion of a pointer to an array
        /// </summary>
        public static explicit operator T[] (Pointer<T> pointer)
        {
            return pointer.IsNull ? null : pointer.ToArray();
        }

        /// <summary>
        /// Explicit conversion of a pointer to a list
        /// </summary>
        public static explicit operator List<T>(Pointer<T> pointer)
        {
            return pointer.IsNull ? null : pointer.ToList();
        }

        #endregion

        #region Value access

        /// <summary>
        /// Read value pointed by the pointer
        /// </summary>
        public static implicit operator T(Pointer<T> source)
        {
            return source.GetValue(0);
        }

        /// <summary>
        /// Try to get the value at an offset of this pointer
        /// </summary>
        public bool TryGetValue(int offset, out T value)
        {
            value = default(T);
            if (Source == null) return false;
            int idx = Index + offset;
            if (idx < 0 || idx >= Source.Count)
                return false;
            value = Source[idx];
            return true;
        }

        /// <summary>
        /// Try to set the value at an offset of this pointer
        /// </summary>
        public bool TrySetValue(T value, int offset)
        {
            if (Source == null) return false;
            int idx = Index + offset;
            if (idx < 0 || idx >= Source.Count)
                return false;
            Source[idx] = value;
            return true;
        }

        /// <summary>
        /// Get the value at an offset of this pointer
        /// </summary>
        /// <exception cref="PointerNullException">If the pointer is null.</exception>
        /// <exception cref="PointerOutOfRangeException">If the real index is out of range of the source of the pointer</exception>
        public T GetValue(int offset = 0)
        {
            var src = Source ?? throw new PointerNullException();
            int idx = Index + offset;
            if (idx < 0 || idx >= Source.Count)
                throw new PointerOutOfRangeException(idx);
            return Source[idx];
        }

        /// <summary>
        /// Set the value at an offset of this pointer
        /// </summary>
        /// <exception cref="PointerNullException">If the pointer is null.</exception>
        /// <exception cref="PointerOutOfRangeException">If the real index is out of range of the source of the pointer</exception>
        public void SetValue(T value, int offset = 0)
        {
            var src = Source ?? throw new PointerNullException();
            int idx = Index + offset;
            if (idx < 0 || idx >= Source.Count)
                throw new PointerOutOfRangeException(idx);
            Source[idx] = value;
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

        /// <summary>
        /// Get/Set value from an offset
        /// </summary>
        public T this[int offset]
        {
            get { return GetValue(offset); }
            set { SetValue(value, offset); }
        }

        /// <summary>
        /// Get/Set the value pointed by this pointer
        /// </summary>
        public T Value
        {
            get => GetValue(0);
            set => SetValue(value, 0);
        }

        #endregion
    }

}
