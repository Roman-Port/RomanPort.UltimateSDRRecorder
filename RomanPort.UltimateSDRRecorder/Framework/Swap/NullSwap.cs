using RomanPort.UltimateSDRRecorder.Framework.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.Framework.Swap
{
    //This swap sits around and does nothing, ignoring even the most basic commands. It is not a productive member of socieity.
    public class NullSwap : ISwap
    {
        public NullSwap(long size) : base(size)
        {
        }

        public override void CopyTo(OutputMultitool s)
        {
            
        }

        public override void Pause()
        {
            
        }

        public override void Resize(long size)
        {
            
        }

        public override void Resume()
        {
            
        }

        public override void Write(byte[] data)
        {
            
        }
    }
}
