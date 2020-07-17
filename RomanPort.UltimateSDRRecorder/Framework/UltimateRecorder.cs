using RomanPort.UltimateSDRRecorder.Framework.Output;
using RomanPort.UltimateSDRRecorder.Framework.Sources;
using RomanPort.UltimateSDRRecorder.Framework.Swap;
using RomanPort.UltimateSDRRecorder.Framework.Ui;
using SDRSharp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RomanPort.UltimateSDRRecorder.Framework.Output.OutputMultitool;

namespace RomanPort.UltimateSDRRecorder.Framework
{
    /// <summary>
    /// Main class controlling this
    /// </summary>
    public class UltimateRecorder
    {
        public ISharpControl control;
        public RecorderControl ui;
        public ISwap swap;
        public ISource source;
        public string title;
        public RecorderSettings settings;

        public int sampleRate;

        public event RecorderOutputHook hooks; //Other clients can hook into the audio output and get the same audio to handle themselves.

        public OutputMultitool activeEncoder;
        public System.Timers.Timer uiUpdateTimer;

        public const int BYTES_PER_SAMPLE = 2;
        public const int CHANNELS = 2;

        public UltimateRecorder(ISharpControl control, RecorderControl ui, ISwap swap, ISource source, string title)
        {
            //Set vars here
            this.control = control;
            this.ui = ui;
            this.swap = swap;
            this.source = source;
            this.title = title;
            this.settings = new RecorderSettings();

            //Claim UI
            ui.recorder = this;
            ui.SetRecorderTitle(title);

            //Reset UI
            ui.SetInterfaceRecordingStatus(false);
            ui.SetBufferStats(0, 0);
            ui.SetRecordStats(TimeSpan.Zero, 0);

            //Create UI timer
            uiUpdateTimer = new System.Timers.Timer(100);
            uiUpdateTimer.AutoReset = true;
            uiUpdateTimer.Elapsed += UiUpdateTimer_Elapsed;
            uiUpdateTimer.Start();

            //Start
            source.Assign(this);
        }

        public delegate void RecorderOutputHook(byte[] data);

        /// <summary>
        /// Returns an OutputMultitool with the completion callback specified. The completionCallback will ALWAYS be called from the UI thread
        /// </summary>
        /// <param name="completionCallback"></param>
        /// <returns></returns>
        private OutputMultitool CreateNewOutput(OutputMultitoolAbortFilePickerBehavior abortBehavior, OutputMultitoolEndEncodingCallback completionCallback)
        {
            OutputMultitool m = new OutputMultitool(sampleRate, CHANNELS, BYTES_PER_SAMPLE * 8, settings.amplification, null, OutputMultitoolDiskSpaceBehavior.BufferToMemory, abortBehavior);
            m.BeginEncoding((OutputMultitoolExitCode exitCode, List<string> filenames) =>
            {
                ui.Invoke((MethodInvoker)delegate
                {
                    completionCallback(exitCode, filenames);
                });
            });
            return m;
        }

        public void SetAmplification(float amp)
        {
            //Set in config
            settings.amplification = amp;

            //Set on encoder, if any
            //TODO
            /*if (activeEncoder != null)
                activeEncoder.activeEncoder.SetAmplificationLevel(amp);*/
        }

        private DriveInfo lastTempDiskCheck;
        private int cyclesSinceLastDiskCheck = 5000;

        private void UiUpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (activeEncoder == null)
                return;

            try
            {
                //Set timer
                double seconds = activeEncoder.bytesWritten / BYTES_PER_SAMPLE / CHANNELS / sampleRate;
                ui.SetRecordStats(new TimeSpan(0, 0, (int)seconds), (float)((double)activeEncoder.bytesWritten / 1024 / 1024));

                //Get free disk space
                cyclesSinceLastDiskCheck++;
                if (cyclesSinceLastDiskCheck > 10)
                {
                    //Refresh
                    lastTempDiskCheck = activeEncoder.GetTempDiskLabel();
                    cyclesSinceLastDiskCheck = 0;
                }

                //Set warning
                string warning = "";
                if (activeEncoder.bytesWaiting > 1500000)
                    warning = $"{Math.Round((double)activeEncoder.bytesWaiting / 1024),0} KB waiting for disk";
                else if(lastTempDiskCheck.AvailableFreeSpace < 5l * 1024l * 1024l * 1024l)
                    warning = $"LOW SPACE ON TEMP DISK ({lastTempDiskCheck.Name.TrimEnd('\\').TrimEnd(':')}): {Math.Round((double)lastTempDiskCheck.AvailableFreeSpace / 1024 / 1024),0} MB";
                ui.SetAlertText(warning);
            } catch (Exception ex)
            {
                //Ignore. Encoder went off while updating
            }
        }

        public void OnAudioReset(int samplerate)
        {
            //Set the sample rate
            this.sampleRate = samplerate;
            SetSwapLengthSeconds(settings.rewind_buffer_length);
        }

        public void OnAudioSamples(byte[] data)
        {
            //Write to the rewind buffer
            swap.Write(data);

            //Write to the file if needed
            if (activeEncoder != null)
                activeEncoder.Write(data);

            //Allow hooks access
            hooks?.Invoke(data);
        }

        public int GetSwapSizeFromSeconds(int seconds)
        {
            return seconds * BYTES_PER_SAMPLE * CHANNELS * sampleRate;
        }

        public void SetSwapLengthSeconds(int seconds)
        {
            //Set in config
            settings.rewind_buffer_length = seconds;

            //Calculate the size, in bytes, of the buffer
            int bytes = GetSwapSizeFromSeconds(seconds);

            //Resize swap
            swap.Resize(bytes);

            //Set UI
            ui.SetBufferStats(seconds, (float)((double)bytes / 1024 / 1024));
        }

        /* UI ACTIONS */

        public void StartRecordingBtnPressed()
        {
            if (activeEncoder != null)
                return;

            //Reset UI
            ui.SetInterfaceRecordingStatus(true);
            ui.SetRecordStats(TimeSpan.Zero, 0);

            //Reset the cycles so that we refresh the disk usage
            cyclesSinceLastDiskCheck = 5000;

            //Create new encoder
            activeEncoder = CreateNewOutput(OutputMultitoolAbortFilePickerBehavior.AlwaysPrompt, (OutputMultitoolExitCode exitCode, List<string> filenames) =>
            {
                //Reset interface
                ui.ClearSavingState();
                ui.SetInterfaceRecordingStatus(false);
                activeEncoder = null;
            });
        }

        public void StopBtnPressed()
        {
            if (activeEncoder == null)
                return;

            //Set saving and then end the encoding
            ui.SetSavingState();
            activeEncoder.EndEncoding();
        }

        public void SaveBufferBtnPressed()
        {
            //Create encoder
            var e = CreateNewOutput(OutputMultitoolAbortFilePickerBehavior.AlwaysDelete, (OutputMultitoolExitCode exitCode, List<string> filenames) =>
            {
                //Reset interface
                ui.ClearSavingState();
                ui.SetInterfaceRecordingStatus(false);

                //If it failed, prompt and delete any files it attempted to create
                if(exitCode == OutputMultitoolExitCode.AbortOutOfDiskSpace || exitCode == OutputMultitoolExitCode.DropOutOfDiskSpace)
                {
                    MessageBox.Show($"Unable to save rewind buffer because your temporary disk is out of space. Free space and try again.", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });

            //Dump the contents of the buffer into it
            swap.CopyTo(e);

            //End
            ui.SetSavingState();
            e.EndEncoding();
        }

        public void SaveBufferContinueBtnPressed()
        {
            //Validate
            if (activeEncoder != null)
                return;

            //Set UI
            ui.SetInterfaceRecordingStatus(true);

            //Create encoder
            cyclesSinceLastDiskCheck = 5000;
            var e = CreateNewOutput(OutputMultitoolAbortFilePickerBehavior.AlwaysPrompt, (OutputMultitoolExitCode exitCode, List<string> filenames) =>
            {
                //Reset interface
                ui.ClearSavingState();
                ui.SetInterfaceRecordingStatus(false);
                activeEncoder = null;
            });

            //Dump the contents of the buffer into it
            swap.CopyTo(e);

            //Redirect here
            activeEncoder = e;
        }
    }
}
