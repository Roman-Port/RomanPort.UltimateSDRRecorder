using SDRSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.DVR.Program
{
    public abstract class IDvrTrigger
    {
        public List<string> settings;

        public IDvrTrigger(List<string> settings)
        {
            this.settings = settings;
        }
        
        /// <summary>
        /// Returns true if the program is authorized to begin recording
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckStart(ISharpControl control);

        /// <summary>
        /// Returns true if the program is authorized to end recording. Only called while recording
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckEnd(ISharpControl control);
    }
}
