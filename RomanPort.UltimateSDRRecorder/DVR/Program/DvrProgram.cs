using RomanPort.UltimateSDRRecorder.DVR.Entities;
using RomanPort.UltimateSDRRecorder.DVR.Interface;
using RomanPort.UltimateSDRRecorder.DVR.Program.Triggers;
using RomanPort.UltimateSDRRecorder.Framework;
using RomanPort.UltimateSDRRecorder.Framework.Output;
using SDRSharp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.DVR.Program
{
    public class DvrProgram
    {
        public DvrProgramProfile profile;
        public UltimateRecorder hostRecorder;
        public SdrDvrInterface dvrInterface;

        public IDvrTrigger trigger;
        public bool isRecording;
        public bool isCancelled;
        public bool isDeleted;
        public DateTime startedRecordingAt;
        public string recordingRadioText;
        public bool recordingRadioTextReady;
        public string recordingFilepath;

        private DvrMultitoolOutputDevice recordingOutput;

        public DvrProgram(DvrProgramProfile profile, UltimateRecorder hostRecorder, SdrDvrInterface dvrInterface)
        {
            this.profile = profile;
            this.hostRecorder = hostRecorder;
            this.dvrInterface = dvrInterface;

            //Create trigger
            switch(profile.trigger_type)
            {
                case SdrProgramProfileTrigger.NullTrigger: trigger = new DvrNullTrigger(profile.trigger_args); break;
                case SdrProgramProfileTrigger.RDSRadioTextTrigger: trigger = new DvrRdsTrigger(profile.trigger_args); break;
                default: throw new Exception("Unsupported trigger type.");
            }
        }

        public long GetBytesWritten()
        {
            if (!isRecording)
                return 0;
            else if (recordingOutput == null)
                return 0;
            else
                return recordingOutput.bytesWritten;
        }

        public long GetBytesWaiting()
        {
            if (recordingOutput == null)
                return 0;
            else
                return recordingOutput.bytesWaiting;
        }

        public TimeSpan GetTimeRecording()
        {
            return DateTime.UtcNow - startedRecordingAt;
        }

        public string GetTimeRecordingString()
        {
            var time = GetTimeRecording();
            return $"{time.Hours.ToString().PadLeft(2, '0')}:{time.Minutes.ToString().PadLeft(2, '0')}:{time.Seconds.ToString().PadLeft(2, '0')}";
        }

        /// <summary>
        /// Ticks occur when it's time to check for changes
        /// </summary>
        public void Tick(ISharpControl control)
        {
            //Check if we have a cancel request. In that case, wait until the trigger ends to remove it
            if(isCancelled)
            {
                if (trigger.CheckEnd(control))
                    isCancelled = false;
                return;
            }

            //Continue as usual
            if(!isRecording)
            {
                //Check if it's time to begin recording
                if(trigger.CheckStart(control))
                {
                    StartRecording(control);
                    return;
                }
            } else
            {
                //Check if we can update the RDS RadioText
                if((DateTime.UtcNow - startedRecordingAt).TotalSeconds > 15 && !recordingRadioTextReady)
                {
                    recordingRadioText = control.RdsRadioText;
                    recordingRadioTextReady = true;
                }
                
                //Check if it's time to stop recording
                if(trigger.CheckEnd(control))
                {
                    StopRecording();
                    return;
                }
            }
        }

        /// <summary>
        /// Removes this....forever
        /// </summary>
        public void DeleteProgram()
        {
            //Disable first
            CancelRecording();
            profile.is_disabled = true;
            isDeleted = true;

            //Remove
            dvrInterface.programs.Remove(this);
            dvrInterface.config.profiles.Remove(profile);

            //Save
            dvrInterface.SaveConfig();
        }

        /// <summary>
        /// Disables the program until we reenable it
        /// </summary>
        public void DisableProgram()
        {
            CancelRecording();
            profile.is_disabled = true;

            //Save
            dvrInterface.SaveConfig();
        }

        /// <summary>
        /// Enables the program again
        /// </summary>
        public void EnableProgram()
        {
            profile.is_disabled = false;

            //Save
            dvrInterface.SaveConfig();
        }

        /// <summary>
        /// Stops a program currently recording and suspends it from doing so until the the trigger ends
        /// </summary>
        public void CancelRecording()
        {
            //Stop recording as usual, but add a flag to prevent it from being reenabled
            if (!isRecording)
                return;
            isCancelled = true;
            StopRecording();
        }

        public void StartRecording(ISharpControl control)
        {
            //Set flags
            isRecording = true;
            startedRecordingAt = DateTime.UtcNow;
            recordingRadioText = control.RdsRadioText;

            //Notify
            dvrInterface.OnProgramBegin(this);

            //Retune
            if (profile.change_freq_enabled)
                control.SetFrequency(profile.change_freq_khz * 1000, false);

            //Get filename
            string filename = profile.output_path;
            for (int i = 0; File.Exists(filename); i++)
                filename = CreateFilenameWithIndex(i);
            recordingFilepath = filename;

            //Create program
            recordingOutput = new DvrMultitoolOutputDevice(this, hostRecorder.sampleRate, UltimateRecorder.CHANNELS, UltimateRecorder.BYTES_PER_SAMPLE * 8, 1, filename);

            //Dump contents of the current swap into this. This'll start it
            hostRecorder.swap.CopyTo(recordingOutput);

            //Hook
            hostRecorder.hooks += HostRecorder_AudioDataHook;
        }

        private void HostRecorder_AudioDataHook(byte[] data)
        {
            recordingOutput.Write(data);
        }

        private string CreateFilenameWithIndex(int index)
        {
            if(profile.output_path.EndsWith(".wav"))
            {
                return profile.output_path.Substring(0, profile.output_path.Length - 4) + "_" + index + ".wav";
            } else
            {
                return profile.output_path + "_I" + index;
            }
        }

        public void StopRecording()
        {
            //Notify
            NotifyEnd();

            //End the buffered stream
            if (recordingOutput.recording)
                recordingOutput.EndEncoding();
        }

        /// <summary>
        /// Notifies clients that we've ended. Used when ending a recording forcefully or gracefully
        /// </summary>
        public void NotifyEnd()
        {
            //Notify
            dvrInterface.OnProgramEnd(this);

            //Unhook
            hostRecorder.hooks -= HostRecorder_AudioDataHook;

            //Set status
            isRecording = false;

            //Set flags
            recordingRadioTextReady = false;
        }
    }
}
