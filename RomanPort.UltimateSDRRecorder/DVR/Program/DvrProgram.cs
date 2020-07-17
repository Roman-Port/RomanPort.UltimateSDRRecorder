﻿using RomanPort.UltimateSDRRecorder.DVR.Entities;
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

        private BufferedOutputStream recordingOutput;
        private WavEncoder recordingEncoder;
        private FileStream recordingFile;

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
            else if (recordingFile == null)
                return 0;
            else
                return recordingFile.Length;
        }

        public long GetBytesWaiting()
        {
            if (!isRecording)
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
            //Get the filename
            string filename = profile.output_path;
            for (int i = 0; File.Exists(filename); i++)
                filename = CreateFilenameWithIndex(i);

            //Set flags
            isRecording = true;
            startedRecordingAt = DateTime.UtcNow;

            //Retune
            if(profile.change_freq_enabled)
                control.SetFrequency(profile.change_freq_khz * 1000, false);

            //Open file
            recordingFile = new FileStream(filename, FileMode.Create);
            recordingEncoder = new WavEncoder(recordingFile, hostRecorder.sampleRate);
            recordingOutput = new BufferedOutputStream(recordingEncoder);

            //Dump contents of the current swap into this
            //hostRecorder.swap.CopyTo(recordingOutput);

            //Hook
            hostRecorder.hooks += HostRecorder_AudioDataHook;

            //Notify
            dvrInterface.OnProgramBegin(this);
        }

        private void HostRecorder_AudioDataHook(byte[] data)
        {
            recordingOutput.Write(data, 0, data.Length);
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
            //Unhook
            hostRecorder.hooks -= HostRecorder_AudioDataHook;

            //End the buffered stream
            recordingOutput.EndRecordingAndWait();

            //Close streams
            recordingEncoder.Flush();
            recordingEncoder.Close();
            recordingFile.Close();
            isRecording = false;

            //Notify
            dvrInterface.OnProgramEnd(this);
        }
    }
}