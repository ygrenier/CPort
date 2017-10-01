using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPort
{
    /// <summary>
    /// C pointer of char simulator
    /// </summary>
    public struct PChar : IPointer, IEnumerable<char>
    {

        #region Ctors
        /// <summary>
        /// Create a new pointer
        /// </summary>
        public PChar(IList<char> source) : this(source, 0)
        { }

        /// <summary>
        /// Create a new pointer with a source and an index
        /// </summary>
        public PChar(IList<char> source, int index)
        {
            Source = source;
            Index = Math.Max(0, source != null ? index : 0);
        }

        /// <summary>
        /// Create a new pointer based on a new buffer of a <paramref name="size"/> elements
        /// </summary>
        public PChar(int size)
        {
            Source = new char[size];
            Index = 0;
        }

        /// <summary>
        /// Create a new pointer from a .Net string
        /// </summary>
        public PChar(string s)
        {
            Index = 0;
            if (s == null)
            {
                Source = null;
            }
            else
            {
                var arr = new char[s.Length + 1];   // Add a '\0'
                s.CopyTo(0, arr, 0, s.Length);
                arr[arr.Length - 1] = '\0';
                Source = arr;
            }
        }
        #endregion

        #region Enumerable
        IEnumerator<char> IEnumerable<char>.GetEnumerator()
        {
            return (Source ?? Enumerable.Empty<char>()).Skip(Index).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<char>)this).GetEnumerator();
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
            if (obj is IList<char>)
            {
                return Source == obj && Index == 0;
            }
            else if (obj is PChar pch)
            {
                return Source == pch.Source && Index == pch.Index;
            }
            else if (obj is Pointer<char> pobj)
            {
                return Source == pobj.Source && Index == pobj.Index;
            }
            return base.Equals(obj);
        }

        #endregion

        #region String management

        /// <summary>
        /// Extract the string
        /// </summary>
        public string GetString()
        {
            if (IsNull) return null;
            return new string(Source.Skip(Index).TakeWhile(c => c != '\0').ToArray());
        }

        #endregion

        #region Casts

        /// <summary>
        /// Implicit conversion of a pointer to a pointer of char
        /// </summary>
        public static implicit operator PChar(Pointer<char> source)
        {
            return source.IsNull ? new PChar() : new PChar(source.Source, source.Index);
        }

        /// <summary>
        /// Implicit conversion of a pointer of char to a pointer
        /// </summary>
        public static implicit operator Pointer<char>(PChar source)
        {
            return source.IsNull ? new Pointer<char>() : new Pointer<char>(source.Source, source.Index);
        }

        /// <summary>
        /// Implicit conversion of a list to a pointer
        /// </summary>
        public static implicit operator PChar(List<char> source)
        {
            return new PChar(source);
        }

        /// <summary>
        /// Implicit conversion of an array to a pointer
        /// </summary>
        public static implicit operator PChar(char[] source)
        {
            return new PChar(source);
        }

        /// <summary>
        /// Implicit conversion from a null pointer
        /// </summary>
        public static implicit operator PChar(NullPointer source)
        {
            return new PChar();
        }

        /// <summary>
        /// Explicit conversion of a pointer to an array
        /// </summary>
        public static explicit operator char[] (PChar pointer)
        {
            return pointer.IsNull ? null : pointer.ToArray();
        }

        /// <summary>
        /// Explicit conversion of a pointer to a list
        /// </summary>
        public static explicit operator List<char>(PChar pointer)
        {
            return pointer.IsNull ? null : pointer.ToList();
        }

        /// <summary>
        /// Implicit conversion of a string to a pointer
        /// </summary>
        public static implicit operator PChar(string s)
        {
            return new PChar(s);
        }

        /// <summary>
        /// Explicit conversion of a pointer to a string
        /// </summary>
        public static explicit operator string(PChar pointer)
        {
            return pointer.GetString();
        }
        #endregion

        #region Pointer manipulation

        /// <summary>
        /// Increment the pointer
        /// </summary>
        public static PChar operator +(PChar source, int offset)
        {
            return new PChar(source.Source, source.Index + offset);
        }

        /// <summary>
        /// Decrement the pointer
        /// </summary>
        public static PChar operator -(PChar source, int offset)
        {
            return new PChar(source.Source, source.Index - offset);
        }

        /// <summary>
        /// Increment the pointer
        /// </summary>
        public static PChar operator ++(PChar source)
        {
            return new PChar(source.Source, source.Index + 1);
        }

        /// <summary>
        /// Decrement the pointer
        /// </summary>
        public static PChar operator --(PChar source)
        {
            return new PChar(source.Source, source.Index - 1);
        }

        #endregion

        #region Equality
        /// <summary>
        /// Test the equality of the pointer with a source
        /// </summary>
        public static bool operator ==(PChar pointer, char[] source)
        {
            return pointer.Source == source && pointer.Index == 0;
        }

        /// <summary>
        /// Test the non equality of the pointer with a source
        /// </summary>
        public static bool operator !=(PChar pointer, char[] source)
        {
            return pointer.Source != source || pointer.Index != 0;
        }

        /// <summary>
        /// Test the equality of the pointer with a source
        /// </summary>
        public static bool operator ==(char[] source, PChar pointer)
        {
            return pointer.Source == source && pointer.Index == 0;
        }

        /// <summary>
        /// Test the non equality of the pointer with a source
        /// </summary>
        public static bool operator !=(char[] source, PChar pointer)
        {
            return pointer.Source != source || pointer.Index != 0;
        }

        /// <summary>
        /// Test pointers equality
        /// </summary>
        public static bool operator ==(PChar p1, PChar p2)
        {
            return p1.Source == p2.Source && p1.Index == p2.Index;
        }

        /// <summary>
        /// Test pointers non equality
        /// </summary>
        public static bool operator !=(PChar p1, PChar p2)
        {
            return p1.Source != p2.Source || p1.Index != p2.Index;
        }

        #endregion

        #region Value access

        /// <summary>
        /// Read value pointed by the pointer
        /// </summary>
        public static implicit operator char(PChar source)
        {
            return source.GetValue(0);
        }

        /// <summary>
        /// Try to get the value at an offset of this pointer
        /// </summary>
        public bool TryGetValue(int offset, out char value)
        {
            value = default(char);
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
        public bool TrySetValue(char value, int offset)
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
        public char GetValue(int offset = 0)
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
        public void SetValue(char value, int offset = 0)
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
        public IList<char> Source { get; private set; }

        /// <summary>
        /// Current index in the source of the pointer
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Get/Set value from an offset
        /// </summary>
        public char this[int offset]
        {
            get { return GetValue(offset); }
            set { SetValue(value, offset); }
        }

        /// <summary>
        /// Get/Set the value pointed by this pointer
        /// </summary>
        public char Value
        {
            get => GetValue(0);
            set => SetValue(value, 0);
        }

        #endregion
    }

}
