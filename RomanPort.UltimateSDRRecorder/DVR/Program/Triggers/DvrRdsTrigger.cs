using RomanPort.UltimateSDRRecorder.DVR.Interface.TriggerConfigDialogs;
using SDRSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.DVR.Program.Triggers
{
    public class DvrRdsTrigger : IDvrTrigger
    {
        public DvrRdsTrigger(List<string> settings) : base(settings)
        {
            //Unpack settings
            triggerText = settings[0];
            autoStopEnabled = bool.Parse(settings[1]);
            autoStopDelaySeconds = int.Parse(settings[2]);
        }

        private string triggerText;
        private bool autoStopEnabled;
        private int autoStopDelaySeconds;

        public DateTime lastSeenTrigger;

        private bool CheckTrigger(ISharpControl control)
        {
            bool found = control.RdsRadioText.ToUpper().Contains(triggerText.ToUpper());
            if (found)
                lastSeenTrigger = DateTime.UtcNow;
            return found;
        }

        public override bool CheckStart(ISharpControl control)
        {
            return CheckTrigger(control);
        }

        public override bool CheckEnd(ISharpControl control)
        {
            //Always return false if auto stop is enabled
            if (!autoStopEnabled)
                return false;
            
            //Check the trigger now, updating the last seen
            bool found = CheckTrigger(control);

            //If there is no auto stop delay, return the current status
            if (autoStopDelaySeconds == 0)
                return found;

            //Return if we haven't seen it in the delay time
            return (DateTime.UtcNow - lastSeenTrigger).TotalSeconds > autoStopDelaySeconds;
        }
    }
}
