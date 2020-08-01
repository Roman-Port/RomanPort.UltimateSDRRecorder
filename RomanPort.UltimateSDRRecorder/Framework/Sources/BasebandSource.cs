using SDRSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.Framework.Sources
{
    public class BasebandSource : ISource
    {
        public BasebandSource() : base(RecordingMode.Baseband, WavSampleFormat.PCM16)
        {

        }
    }
}
