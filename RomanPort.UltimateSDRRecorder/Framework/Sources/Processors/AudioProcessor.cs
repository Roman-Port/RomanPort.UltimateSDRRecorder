﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDRSharp.Radio;

namespace RomanPort.UltimateSDRRecorder.Framework.Sources.Processors
{
    public class AudioProcessor : IRealProcessor, IStreamProcessor, IBaseProcessor
    {
        private double _sampleRate;
        private bool _enabled;

        public event AudioProcessor.AudioReadyDelegate AudioReady;

        public bool Enabled
        {
            get
            {
                return this._enabled;
            }
            set
            {
                this._enabled = value;
            }
        }

        public double SampleRate
        {
            get
            {
                return this._sampleRate;
            }
            set
            {
                this._sampleRate = value;
            }
        }

        public unsafe void Process(float* audio, int length)
        {
            AudioProcessor.AudioReadyDelegate audioReady = this.AudioReady;
            if (audioReady == null)
                return;
            audioReady(audio, length);
        }

        public unsafe delegate void AudioReadyDelegate(float* audio, int length);
    }
}
