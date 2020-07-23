using SDRSharp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.Framework.Sources
{
    public class AudioGainTester
    {
        private AudioSource source;
        private bool reset;

        public bool clipping;
        public float amplification;
        public event AudioClipEvent ClippingChangedEvent;

        public void BeginTest(ISharpControl control)
        {
            source = new AudioSource();
            source.AudioStartedEvent += Source_AudioStartedEvent;
            source.AudioRecievedEvent += Source_AudioRecievedEvent;
            source._SetSettings(control);
            source.StartRecording();
        }

        private void Source_AudioRecievedEvent(byte[] data)
        {
            //Amplify and test
            bool clipping = RecorderTools.AmplifyPCMSamples(data, 0, data.Length, 16, amplification);
            if(this.clipping != clipping || reset)
            {
                ClippingChangedEvent?.Invoke(clipping);
                this.clipping = clipping;
                this.reset = false;
            }
        }

        private void Source_AudioStartedEvent(int sampleRate)
        {
            
        }

        public void EndTest()
        {
            source.StopRecording();
        }

        public void ChangeAmplification(float amp)
        {
            amplification = amp;
            reset = true;
        }

        public delegate void AudioClipEvent(bool clipping);
    }
}
