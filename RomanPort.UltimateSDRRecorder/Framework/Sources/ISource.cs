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
