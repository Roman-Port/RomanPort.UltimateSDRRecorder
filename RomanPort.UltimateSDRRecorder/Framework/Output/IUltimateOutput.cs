using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.Framework.Output
{
    public abstract class IUltimateOutput
    {
        public int sampleRate;

        public DateTime start;
        public long totalBytes;

        public IUltimateOutput(int sampleRate)
        {
            this.sampleRate = sampleRate;
            start = DateTime.UtcNow;
        }

        public abstract void OnGetSamples(byte[] data);
        public abstract void OnEndSession();
    }
}
