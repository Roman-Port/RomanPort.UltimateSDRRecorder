using RomanPort.UltimateSDRRecorder.Framework.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder.Framework.Output.OutputDevices
{
    /// <summary>
    /// Device used by the "save buffer" option in the recorder
    /// </summary>
    public class RecorderSaveBufferOutputDevice : OutputMultitool
    {
        public RecorderSaveBufferOutputDevice(RecorderControl ui, int sampleRate, short channels, short bitsPerSample, float amplification, string requestedFilename) : base(sampleRate, channels, bitsPerSample, amplification, requestedFilename)
        {
            this.ui = ui;
        }

        private RecorderControl ui;

        public override void OnEncodingFinished()
        {
            //Reset interface
            ui.ClearSavingState();
            ui.SetInterfaceRecordingStatus(false);
        }

        public override void OnBeginSaving()
        {
            ui.SetSavingState();
        }

        public override OutputMultitoolIoErrorAction HandleIOError()
        {
            return OutputMultitoolIoErrorAction.AbortRecording;
        }

        public override void OnAborted()
        {
            MessageBox.Show($"Unable to save the rewind buffer because your temporary disk is out of space. Free up space and try again.", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public override string GetAbortedOutputFile()
        {
            return null;
        }
    }
}
