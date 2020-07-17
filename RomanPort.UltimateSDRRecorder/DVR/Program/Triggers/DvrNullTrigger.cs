using SDRSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.DVR.Program.Triggers
{
    public class DvrNullTrigger : IDvrTrigger
    {
        public DvrNullTrigger(List<string> settings) : base(settings)
        {
        }

        public override bool CheckEnd(ISharpControl control)
        {
            return false;
        }

        public override bool CheckStart(ISharpControl control)
        {
            return false;
        }
    }
}
