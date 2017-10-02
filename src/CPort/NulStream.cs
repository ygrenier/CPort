using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CPort
{
    /// <summary>
    /// Empty read/write stream
    /// </summary>
    public class NulStream : Stream
    {
        public override void Flush()
        { }

        public override int Read(byte[] buffer, int offset, int count)
        => 0;

        public override long Seek(long offset, SeekOrigin origin)
        => throw new InvalidOperationException();

        public override void SetLength(long value)
        => throw new InvalidOperationException();

        public override void Write(byte[] buffer, int offset, int count)
        { }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => 0;

        public override long Position { get => 0; set => throw new InvalidOperationException(); }
    }

}
