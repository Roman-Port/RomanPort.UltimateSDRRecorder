using RomanPort.UltimateSDRRecorder.Framework.Output;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.Framework.Swap
{
    public abstract class ISwap
    {
        public long size;
        public event SwapWriteErrorEventArgs OnSwapWriteError;

        public ISwap(long size)
        {
            this.size = size;
        }
        public abstract void Write(byte[] data);
        public abstract void CopyTo(OutputMultitool s);
        public abstract void Resize(long size);
        public abstract void Pause();
        public abstract void Resume();

        internal void TriggerSwapWriteError()
        {
            Pause();
            OnSwapWriteError.Invoke();
        }

        public delegate void SwapWriteErrorEventArgs();
    }
}
