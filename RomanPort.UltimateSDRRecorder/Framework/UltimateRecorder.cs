using RomanPort.UltimateSDRRecorder.Framework.Entities;
using RomanPort.UltimateSDRRecorder.Framework.Output;
using RomanPort.UltimateSDRRecorder.Framework.Output.OutputDevices;
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
        public List<RdsAutoNameHistoryName> rdsRadioTextHistory; //Stores the history of the RadioText field so we can use it to auto-name later

        public int sampleRate;

        public event RecorderOutputHook hooks; //Other clients can hook into the audio output and get the same audio to handle themselves.

        public OutputMultitool activeEncoder;
        public bool isRecording;
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
            rdsRadioTextHistory = new List<RdsAutoNameHistoryName>();

            //Load config
            settings = ConfigFileManager.LoadConfigFile<RecorderSettings>("RECORDER_" + this.title.ToUpper());
            if (settings == null)
                settings = new RecorderSettings();

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

            //Add evvent to swap
            swap.OnSwapWriteError += Swap_OnSwapWriteError;

            //Set settings
            SetAmplification(settings.amplification);

            //Start
            source.Assign(this);
        }

        private void Swap_OnSwapWriteError()
        {
            if(settings.rewind_buffer_length == 10)
            {
                //We've already tried resizing it. Disable it.
                swap = new NullSwap(0);

                //Notify the user
                RecorderTools.NotifyUserNewThread("Rewind Buffer Error", "There was an error with the rewind buffer and it has been disabled. This is caused by the system running out of disk space or RAM.\n\nThe rewind buffer has been resized to a smaller size. Restart SDRSharp to reenable the rewind buffer.", MessageBoxIcon.Error);
            } else
            {
                //Try resizing it
                settings.rewind_buffer_length = 10;
                SetSwapLengthSeconds(settings.rewind_buffer_length);
                SaveSettings();

                //Notify the user
                RecorderTools.NotifyUserNewThread("Rewind Buffer Error", "There was an error with the rewind buffer. This is caused by the system running out of disk space or RAM.\n\nThe rewind buffer has been resized to a smaller size.", MessageBoxIcon.Warning);
            }           
        }

        public delegate void RecorderOutputHook(byte[] data);

        public void SaveSettings()
        {
            ConfigFileManager.SaveConfigFile("RECORDER_" + this.title.ToUpper(), settings);
        }

        public void SetAmplification(float amp)
        {
            //Set in config
            settings.amplification = amp;

            //Set on encoder, if any
            source.amplification = amp;
        }

        private DriveInfo lastTempDiskCheck;
        private int cyclesSinceLastDiskCheck = 5000;

        private void UiUpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (isRecording == false)
                return;

            try
            {
                //Update RDS history
                if(control.RdsRadioText.Length > 0)
                {
                    _UpdateRDSHistory(control.RdsRadioText);
                }
                
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
                else if(lastTempDiskCheck.AvailableFreeSpace < 1l * 1024l * 1024l * 1024l)
                    warning = $"LOW SPACE ON DISK ({lastTempDiskCheck.Name.TrimEnd('\\').TrimEnd(':')}): {Math.Round((double)lastTempDiskCheck.AvailableFreeSpace / 1024 / 1024),0} MB";
                ui.SetAlertText(warning);
            } catch (Exception ex)
            {
                //Ignore. Encoder went off while updating
            }
        }

        private void _UpdateRDSHistory(string radioText)
        {
            //If this starts with a space, it's probably an invalid RDS name
            if (radioText.StartsWith(" ") || radioText.Contains("  "))
                return;
            
            //Find if we don't need to update it
            if (rdsRadioTextHistory.Count != 0)
            {
                if (radioText.StartsWith(rdsRadioTextHistory[rdsRadioTextHistory.Count - 1].text))
                {
                    rdsRadioTextHistory[rdsRadioTextHistory.Count - 1].text = radioText;
                    return;
                }
            }

            //Insert new
            lock (rdsRadioTextHistory) {
                rdsRadioTextHistory.Add(new RdsAutoNameHistoryName
                {
                    seen = DateTime.UtcNow,
                    text = radioText
                });
            }
        }

        public void OnAudioReset(int samplerate)
        {
            //Set the sample rate
            this.sampleRate = samplerate;

            //Check if the swap size will be very big
            long swapSize = GetSwapSizeFromSeconds(settings.rewind_buffer_length);
            if(swapSize > 1024 * 1024 * 1024) //> 1GB
            {
                //Alert the user and prompt them to turn it down
                var d = MessageBox.Show($"The size of your recording rewind buffer is very large. This may result in the program crashing. Would you like to reset it to a smaller size? The current size is {RecorderTools.GetMegabytes(swapSize, 0)} MB.", "Rewind Buffer Very Large", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(d == DialogResult.Yes)
                {
                    settings.rewind_buffer_length = 20;
                    SaveSettings();
                    RecorderTools.NotifyUserNewThread("Rewind Buffer Protection", $"The rewind buffer has been resized to {settings.rewind_buffer_length} seconds. You can manually increase the size in settings.", MessageBoxIcon.Information);
                }
            }

            //Update swap size
            SetSwapLengthSeconds(settings.rewind_buffer_length);
        }

        public void OnAudioSamples(byte[] data)
        {
            //Write to the rewind buffer
            swap.Write(data);

            //Write to the file if needed
            if (isRecording)
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

            //Set UI
            if (ui.Created)
            {
                ui.Invoke((MethodInvoker)delegate
                {
                    ui.SetBufferStats(seconds, (float)((double)bytes / 1024 / 1024));
                });
            }

            //Resize swap
            swap.Resize(bytes);
        }

        /* UI ACTIONS */

        public void StartRecordingBtnPressed()
        {
            if (isRecording)
                return;

            //Start refreshing UI
            uiUpdateTimer.Start();

            //Create new encoder
            activeEncoder = CreateStandardReccorder();
            isRecording = true;
            activeEncoder.BeginEncoding();
        }

        public void StopBtnPressed(bool rdsName = false)
        {
            if (!isRecording)
                return;
            string newName = null;

            //If RDS AutoName is on, create a save path
            if(rdsName)
            {
                if (!GetRdsAutoNameFilename(out newName))
                    return;
            }

            //Stop updating UI
            uiUpdateTimer.Stop();

            //Set saving and then end the encoding
            isRecording = false;
            activeEncoder.EndEncoding(newName);
        }

        public void SaveBufferBtnPressed()
        {
            //Create encoder
            var e = new RecorderSaveBufferOutputDevice(ui, sampleRate, CHANNELS, BYTES_PER_SAMPLE * 8, settings.amplification, null);

            //Dump the contents of the buffer into it. This'll start the recording
            swap.CopyTo(e);

            //End
            ui.SetSavingState();
            e.EndEncoding();
        }

        public void SaveBufferContinueBtnPressed()
        {
            //Validate
            if (isRecording)
                return;

            //Create recorder
            var e = CreateStandardReccorder();

            //Redirect here
            activeEncoder = e;
            isRecording = true;

            //Start refreshing UI
            uiUpdateTimer.Start();

            //Dump the contents of the buffer into it. This'll start the recording
            swap.CopyTo(e);
        }

        private RecorderStandardOutputDevice CreateStandardReccorder()
        {
            //Set UI
            ui.SetInterfaceRecordingStatus(true);
            ui.SetRecordStats(TimeSpan.Zero, 0);

            //Reset the cycles so that we refresh the disk usage
            cyclesSinceLastDiskCheck = 5000;

            //Create encoder
            var e = new RecorderStandardOutputDevice(this, sampleRate, CHANNELS, BYTES_PER_SAMPLE * 8, settings.amplification, null);
            return e;
        }

        /// <summary>
        /// Prompts the user to choose the RDS name to use. Returns false if it is cancelled
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private bool GetRdsAutoNameFilename(out string filename)
        {
            filename = null;

            //Check if we even have RT-Text
            if (rdsRadioTextHistory.Count == 0)
            {
                MessageBox.Show("There is no RDS RadioText recieved at the moment. Cannot automatically save using Radio Text name.", "No RDS Text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check if we have an output folder
            if (settings.rds_autoname_output_dir.Length == 0)
            {
                MessageBox.Show("The RDS auto name output folder is not set. Please choose an output folder in settings and try again.", "No RDS Auto Name Output Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Try to determine which RT Text to use
            RdsAutoNameHistoryName name = rdsRadioTextHistory[rdsRadioTextHistory.Count - 1];
            for (int i = rdsRadioTextHistory.Count - 1; i >= 0; i--)
            {
                if ((DateTime.UtcNow - rdsRadioTextHistory[i].seen).TotalSeconds > 30)
                {
                    name = rdsRadioTextHistory[i];
                    break;
                }
            }

            //Prompt user what RT RadioText to use
            RdsAutoNameForm form = new RdsAutoNameForm(rdsRadioTextHistory, name);
            var result = form.ShowDialog();

            //Handle form
            if (result == DialogResult.Yes)
            {
                //We'll use the selecetd name
                //We'll now translate the RDS name to be a filename supported name as well as stripping out unnessessary data
                filename = form.chosenName.Replace("Now playing ", "");
                foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                {
                    filename = filename.Replace(c, '_');
                }

                //Now make the real path
                filename = settings.rds_autoname_output_dir + filename + ".wav";
                return true;
            }
            else if (result == DialogResult.No)
            {
                //We'll manually select a name
                filename = null;
                return true;
            }
            else
            {
                //Cancel
                return false;
            }
        }
    }
}
