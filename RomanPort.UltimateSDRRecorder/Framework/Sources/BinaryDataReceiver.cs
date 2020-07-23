using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDRSharp.Radio;
using System.Threading;
using System.Runtime.InteropServices;
using SDRSharp.Common;
using RomanPort.UltimateSDRRecorder.Framework.Sources.Processors;

namespace RomanPort.UltimateSDRRecorder.Framework.Sources
{
    /// <summary>
    /// Actually gets the binary audio data, the sends it out in a reasonable way
    /// </summary>
    public abstract class BinaryDataReceiver
    {
        public double _sampleRate;
        private double _frequencyOffset;
        public WavSampleFormat _wavSampleFormat;
        private FrequencyTranslator _iqTranslator;
        public RecordingMode _recordingMode;
        private IQProcessor _iqProcessor;
        private AudioProcessor _audioProcessor;
        internal ISharpControl control;
        public float amplification = 1;

        public double SampleRate { get; set; }

        public int FrequencyOffset { get; set; }

        public BinaryDataReceiver()
        {
            
            
        }

        public unsafe void SetSettings(ISharpControl control, RecordingMode mode, WavSampleFormat format)
        {
            this.control = control;
            if (mode == RecordingMode.Audio)
            {
                _audioProcessor = new AudioProcessor();
                _audioProcessor.Enabled = true;
                control.RegisterStreamHook((object)this._audioProcessor, ProcessorType.FilteredAudioOutput);
                SampleRate = (double)control.AudioSampleRate;
            }
            else
            {
                _iqProcessor = new IQProcessor();
                _iqProcessor.Enabled = true;
                control.RegisterStreamHook((object)this._iqProcessor, ProcessorType.RawIQ);
                SampleRate = control.RFBandwidth;
                FrequencyOffset = control.IFOffset;
            }
            this._recordingMode = mode;
            this._wavSampleFormat = format;
        }

        public unsafe void IQSamplesIn(Complex* buffer, int length)
        {
            //Translate. Not entirely sure how this works or what it even *does*
            //There's a high chance this might break plugins further down in the chain. If it does, let me know
            if (this._iqTranslator == null || this._iqTranslator.SampleRate != this._sampleRate || this._iqTranslator.Frequency != this._frequencyOffset)
            {
                this._iqTranslator = new FrequencyTranslator(this._sampleRate);
                this._iqTranslator.Frequency = this._frequencyOffset;
            }
            this._iqTranslator.Process(buffer, length);

            //Write
            OnWriteUnsafeBinary((float*)buffer, length);
        }

        //Length is the number of pairs. Len1 = 8 bytes, 2 floats
        public unsafe void AudioSamplesIn(float* audio, int length)
        {
            //Create buffer
            float[] buf = new float[length * 2];
            var uBuf = UnsafeBuffer.Create(buf);
            Utils.Memcpy(uBuf, audio, length * 4);
            
            //Scale
            this.ScaleAudio((float*)uBuf, length);

            //Write
            OnWriteUnsafeBinary((float*)uBuf, length / 2);
        }

        public unsafe void ScaleAudio(float* audio, int length)
        {
            for (int index = 0; index < length; ++index)
            {
                float* numPtr = audio + index;
                *numPtr = *numPtr * (float)Math.Pow(3.0, 10.0) * amplification;
            }
        }

        public unsafe void StartRecording()
        {
            if (this._recordingMode == RecordingMode.Baseband)
            {
                this._iqProcessor.IQReady += new IQProcessor.IQReadyDelegate(this.IQSamplesIn);
                this._iqProcessor.Enabled = true;
            }
            else
            {
                this._audioProcessor.AudioReady += new AudioProcessor.AudioReadyDelegate(this.AudioSamplesIn);
                this._audioProcessor.Enabled = true;
            }
        }

        public unsafe void StopRecording()
        {
            if (this._recordingMode == RecordingMode.Baseband)
            {
                this._iqProcessor.Enabled = false;
                this._iqProcessor.IQReady -= new IQProcessor.IQReadyDelegate(this.IQSamplesIn);
            }
            else
            {
                this._audioProcessor.Enabled = false;
                this._audioProcessor.AudioReady -= new AudioProcessor.AudioReadyDelegate(this.AudioSamplesIn);
            }
            if (_audioProcessor != null)
                control.UnregisterStreamHook(_audioProcessor);
            if (_iqProcessor != null)
                control.UnregisterStreamHook(_iqProcessor);
        }

        //BINARY BIT
        public unsafe void OnWriteUnsafeBinary(float* data, int length)
        {           
            //Get samples in their binary form
            byte[] buffer;
            switch (this._wavSampleFormat)
            {
                case WavSampleFormat.PCM8:
                    buffer = this.GetPCM8(data, length);
                    break;
                case WavSampleFormat.PCM16:
                    buffer = this.GetPCM16(data, length);
                    break;
                case WavSampleFormat.Float32:
                    buffer = this.GetFloat(data, length);
                    break;
                default:
                    return;
            }
            OnGetSamples(buffer, length);
        }

        public abstract void OnGetSamples(byte[] buffer, int count);

        private unsafe byte[] GetPCM8(float* data, int length)
        {
            byte[] outputBuffer = new byte[length * 2];
            float* numPtr1 = data;
            for (int index1 = 0; index1 < length; ++index1)
            {
                byte[] outputBuffer1 = outputBuffer;
                int index2 = index1 * 2;
                float* numPtr2 = numPtr1;
                float* numPtr3 = (float*)((IntPtr)numPtr2 + 4);
                int num1 = (int)(byte)((double)*numPtr2 * (double)sbyte.MaxValue + 128.0);
                outputBuffer1[index2] = (byte)num1;
                byte[] outputBuffer2 = outputBuffer;
                int index3 = index1 * 2 + 1;
                float* numPtr4 = numPtr3;
                numPtr1 = (float*)((IntPtr)numPtr4 + 4);
                int num2 = (int)(byte)((double)*numPtr4 * (double)sbyte.MaxValue + 128.0);
                outputBuffer2[index3] = (byte)num2;
            }
            return outputBuffer;
        }

        private unsafe byte[] GetPCM16(float* data, int length)
        {
            byte[] outputBuffer = new byte[length * 2 * 2];
            float* numPtr1 = data;
            for (int index = 0; index < length; ++index)
            {
                float* numPtr2 = numPtr1;
                float* numPtr3 = (float*)((IntPtr)numPtr2 + 4);
                short num1 = (short)((double)*numPtr2 * (double)short.MaxValue);
                float* numPtr4 = numPtr3;
                numPtr1 = (float*)((IntPtr)numPtr4 + 4);
                short num2 = (short)((double)*numPtr4 * (double)short.MaxValue);
                outputBuffer[index * 4] = (byte)((uint)num1 & (uint)byte.MaxValue);
                outputBuffer[index * 4 + 1] = (byte)((uint)num1 >> 8);
                outputBuffer[index * 4 + 2] = (byte)((uint)num2 & (uint)byte.MaxValue);
                outputBuffer[index * 4 + 3] = (byte)((uint)num2 >> 8);
            }
            return outputBuffer;
        }

        private unsafe byte[] GetFloat(float* data, int length)
        {
            byte[] outputBuffer = new byte[length * 4 * 2];
            Marshal.Copy((IntPtr)(void*)data, outputBuffer, 0, outputBuffer.Length);
            return outputBuffer;
        }
    }

    public enum WavSampleFormat
    {
        PCM8 = 0,
        PCM16 = 1,
        Float32 = 2
    }

    public enum RecordingMode
    {
        Baseband,
        Audio
    }
}
