using RomanPort.UltimateSDRRecorder.Framework.Output;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.Framework.Swap
{
    public class MemorySwap : ISwap
    {
        private byte[] buffer;
        private long bufferPos;
        private bool bufferFull;
        private bool isLocked;

        public MemorySwap(long size) : base(size)
        {
            Resize(size);
        }

        public override void CopyTo(OutputMultitool s)
        {
            //How this behaves will differ if the buffer is full or not
            if (bufferFull)
            {
                //Get the number of bytes until the end and copy those first. Then, copy the other part of the array
                long remainingBytes = size - bufferPos;

                //Copy the remaining bytes, as these are the ones ahead of us (that we would be overwriting), then copy the bytes behind this point that we just wrote
                s.BeginEncoding(buffer, new WriteIndexParams[] {
                    new WriteIndexParams((int)bufferPos, (int)remainingBytes),
                    new WriteIndexParams(0, (int)bufferPos)
                }, ToggleWriteLock);
            }
            else
            {
                //We can just copy all of the bytes, starting at the beginning
                s.BeginEncoding(buffer, new WriteIndexParams[] {
                    new WriteIndexParams(0, (int)bufferPos)
                }, ToggleWriteLock);
            } 
        }

        private void ToggleWriteLock(bool locked)
        {
            if (locked)
                Pause();
            else
                Resume();
        }

        public override void Resize(long size)
        {
            this.size = size;
            buffer = new byte[size];
            bufferPos = 0;
            bufferFull = false;
        }

        public override void Write(byte[] data)
        {
            //If locked, prevent writing
            if (isLocked)
                return;
            
            //Write
            for(int i = 0; i<data.Length; i+=1)
            {
                buffer[(bufferPos + i) % size] = data[i];
            }

            //If we overflowed, set that flag
            if(bufferPos + data.Length > size)
            {
                bufferFull = true;
            }

            //Modify
            bufferPos = (bufferPos + data.Length) % size;
        }

        public override void Pause()
        {
            isLocked = true;
        }

        public override void Resume()
        {
            isLocked = false;
        }
    }
}
