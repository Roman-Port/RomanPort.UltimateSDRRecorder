using RomanPort.UltimateSDRRecorder.Framework;
using RomanPort.UltimateSDRRecorder.Framework.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.DVR.Program
{
    public class DvrMultitoolOutputDevice : OutputMultitool
    {
        public DvrMultitoolOutputDevice(DvrProgram program, int sampleRate, short channels, short bitsPerSample, float amplification, string requestedFilename) : base(sampleRate, channels, bitsPerSample, amplification, requestedFilename)
        {
            this.program = program;
        }

        private DvrProgram program;

        public override string GetAbortedOutputFile()
        {
            //This should never be called.
            throw new NotImplementedException();
        }

        public override OutputMultitoolIoErrorAction HandleIOError()
        {
            return OutputMultitoolIoErrorAction.AbortRecording;
        }

        public override void OnAborted()
        {
            //Notify
            program.NotifyEnd();

            //Alert the user
            RecorderTools.NotifyUserNewThread("DVR Recording Error", $"The active DVR program \"{program.profile.program_title}\" was ended early (at {DateTime.Now.ToLongTimeString()} {DateTime.Now.ToShortDateString()}) because you are out of disk space on the output disk.", System.Windows.Forms.MessageBoxIcon.Error);
        }

        public override void OnBeginSaving()
        {
            //Discard
        }

        public override void OnEncodingFinished()
        {
            //Discard
        }
    }
}
