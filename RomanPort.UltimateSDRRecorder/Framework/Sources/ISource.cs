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
        public event AudioEndedEventArgs AudioEndedEvent;
        public event AudioRecievedEventArgs AudioRecievedEvent;

        public ISource(RecordingMode mode, WavSampleFormat format) : base(mode, format)
        {

        }

        public void Assign(UltimateRecorder recorder)
        {
            //Set binary settings
            SetSettings(recorder.control);

            //Add events
            AudioRecievedEvent += recorder.OnAudioSamples;
            AudioStartedEvent += recorder.OnAudioReset;
            AudioEndedEvent += recorder.OnAudioEnded;
            recorder.control.PropertyChanged += Control_PropertyChanged;
        }

        private void Control_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "StartRadio")
            {
                StartRecording();
                AudioStartedEvent((int)SampleRate);
            } else if (e.PropertyName == "StopRadio")
            {
                StopRecording();
                AudioEndedEvent?.Invoke();
            }
        }

        public override void OnGetSamples(byte[] buffer, int count)
        {
            AudioRecievedEvent(buffer);
        }

        public delegate void AudioStartedEventArgs(int sampleRate);
        public delegate void AudioEndedEventArgs();
        public delegate void AudioRecievedEventArgs(byte[] data);
    }
}
