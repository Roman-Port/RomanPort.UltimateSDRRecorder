using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.Framework.Output
{
    /// <summary>
    /// Creates a stream you can only write to
    /// </summary>
    public abstract class IOneWayStream : Stream
    {
        public Stream underlyingStream;
        public bool opened;

        public IOneWayStream(Stream underlyingStream)
        {
            this.underlyingStream = underlyingStream;
            opened = true;
        }
        
        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => underlyingStream.Length;

        public override long Position { get => underlyingStream.Position; set => throw new NotSupportedException(); }

        public override void Flush()
        {
            underlyingStream.Flush();
        }

        public override void Close()
        {
            opened = false;
            base.Close();
            underlyingStream.Close();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return underlyingStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            underlyingStream.SetLength(value);
        }
    }
}
