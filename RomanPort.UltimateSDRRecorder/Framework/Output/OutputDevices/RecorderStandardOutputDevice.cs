using RomanPort.UltimateSDRRecorder.Framework.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder.Framework.Output.OutputDevices
{
    /// <summary>
    /// Device used by the "save buffer" option in the recorder
    /// </summary>
    public class RecorderStandardOutputDevice : OutputMultitool
    {
        public RecorderStandardOutputDevice(UltimateRecorder recorder, int sampleRate, short channels, short bitsPerSample, float amplification, string requestedFilename) : base(sampleRate, channels, bitsPerSample, amplification, requestedFilename)
        {
            this.recorder = recorder;
            this.ui = recorder.ui;
        }

        //public const long MAX_BUFFERED_BYTES = 209715200;
        public const long MAX_BUFFERED_BYTES = 209715;

        private UltimateRecorder recorder;
        private RecorderControl ui;

        public bool swapBufferRequested; //If true, it means the user pressed the "buffer+record" button

        private bool userWarned;

        public override void OnEncodingFinished()
        {
            ui.Invoke((MethodInvoker)delegate
            {
                //Reset interface
                ui.ClearSavingState();
                ui.SetInterfaceRecordingStatus(false);
            });
        }

        public override void OnBeginSaving()
        {
            ui.Invoke((MethodInvoker)delegate
            {
                ui.SetSavingState();
            });
        }

        public override OutputMultitoolIoErrorAction HandleIOError()
        {
            //Warn user they are out of disk space
            if (!userWarned)
                ShowDiskWarningDialig();
            userWarned = true;

            //Begin dropping buffers when we get too much in memory
            if (bytesWaiting > MAX_BUFFERED_BYTES)
                return OutputMultitoolIoErrorAction.AbortRecording;
            return OutputMultitoolIoErrorAction.RetrySample;
        }

        private void ShowDiskWarningDialig()
        {
            //Summon a new thread to show the warning
            Thread t = new Thread(() =>
            {
                MessageBox.Show($"Quickly free up disk space on your temporary disk ({GetTempDiskLabel().Name}) to continue recording without interruption.", "Out of Disk Space", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            });
            t.IsBackground = true;
            t.Start();
        }

        public override void OnAborted()
        {
            if(!hasBlockingWriteBeenCompleted && swapBufferRequested) //Check if we failed to write even the swap buffer
                MessageBox.Show($"Unable to save rewind buffer because your temporary disk is out of space. Free space and try again.", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if(filenames.Count == 0)
                MessageBox.Show($"Unable to begin recording because your temporary disk is out of space. Free space and try again.", "Recording Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show($"Recording ended because your temporary disk is out of space. Choose where to save the recorded data.", "Recording Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public override string GetAbortedOutputFile()
        {
            //Choose when to delete it
            if ((!hasBlockingWriteBeenCompleted && swapBufferRequested) || filenames.Count == 0)
                return null;

            //Open dialog
            return OpenFilePicker("wav");
        }
    }
}
