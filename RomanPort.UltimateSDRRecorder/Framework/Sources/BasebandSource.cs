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
        public override void _SetSettings(ISharpControl control)
        {
            SetSettings(control, RecordingMode.Baseband, WavSampleFormat.PCM16);
        }
    }
}
