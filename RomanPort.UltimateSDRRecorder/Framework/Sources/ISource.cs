using SDRSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.Framework.Sources
{
    /// <summary>
    /// An audio source. Does not do anything until Assign is called
    /// </summary>
    public abstract class ISource : BinaryDataReceiver
    {
        public event AudioStartedEventArgs AudioStartedEvent;
        public event AudioRecievedEventArgs AudioRecievedEvent;

        public ISource() : base()
        {

        }

        public void Assign(UltimateRecorder recorder)
        {
            //Set binary settings
            _SetSettings(recorder.control);

            //Add events
            AudioRecievedEvent += recorder.OnAudioSamples;
            AudioStartedEvent += recorder.OnAudioReset;

            //Set up
            AudioStartedEvent((int)SampleRate);

            //Start
            StartRecording();

            recorder.control.PropertyChanged += Control_PropertyChanged;
        }

        private void Control_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "StartRadio")
            {
                StopRecording();
                if (_recordingMode == RecordingMode.Audio)
                {
                    SampleRate = (double)control.AudioSampleRate;
                }
                else
                {
                    SampleRate = control.RFBandwidth;
                    FrequencyOffset = control.IFOffset;
                }
                AudioStartedEvent((int)SampleRate);
                StartRecording();
            } else if (e.PropertyName == "StopRadio")
            {
                StopRecording();
            }
        }

        public abstract void _SetSettings(ISharpControl control);

        public override void OnGetSamples(byte[] buffer, int count)
        {
            AudioRecievedEvent(buffer);
        }

        public delegate void AudioStartedEventArgs(int sampleRate);
        public delegate void AudioRecievedEventArgs(byte[] data);
    }
}
