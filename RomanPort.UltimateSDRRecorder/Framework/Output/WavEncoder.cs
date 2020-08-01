using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.Framework.Output
{
    public class WavEncoder : IOneWayStream
    {
        public const int BITS_PER_SAMPLE = 16;
        public const int CHANNELS = 2;
        public const int WAV_HEADER_SIZE = 44;

        public int sampleRate;

        public long fileSizeOffs;
        public long dataSizeOffs;
        public int audioLength = 0;

        public float amplification = 2;
        public event WavClipEventArgs ClipEvent;
        public bool clipped;

        public WavEncoder(Stream output, int sampleRate) : base(output)
        {
            //Set
            this.sampleRate = sampleRate;

            //Write header
            WriteHeader();
        }

        public delegate void WavClipEventArgs(WavEncoder wav, bool clipping);

        public void SetAmplificationLevel(float amp)
        {
            amplification = amp;
            clipped = false;
        }

        /// <summary>
        /// Returns if this file can fit the data supplied
        /// </summary>
        /// <param name="dataLength"></param>
        /// <returns></returns>
        public bool CanFitData(long dataLength)
        {
            //Check if it overflows
            return dataLength + (long)audioLength < int.MaxValue;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            //Make sure it will fit without causing an overflow
            if(audioLength + count < audioLength)
            {
                //throw new Exception("There is not enough space to fit the audio buffer.");
            }

            //If we've ended, abort
            if (!opened)
                return;

            //Do amplification if needed
            if(amplification != 1)
            {
                //Amplify two bytes at a time
                if(BITS_PER_SAMPLE == 16)
                {
                    //Validate
                    if (count % 2 != 0)
                        throw new Exception("Cannot amplify, as there are an odd number of bytes.");

                    //Multiply
                    short working;
                    float workingF;
                    bool clipped = false;
                    for(int i = 0; i<count/2; i++)
                    {
                        workingF = (float)BitConverter.ToInt16(buffer, offset + (i * 2)) * amplification;
                        clipped = workingF > short.MaxValue || workingF < short.MinValue || clipped;
                        if (workingF > short.MaxValue)
                            workingF = short.MaxValue;
                        if (workingF < short.MinValue)
                            workingF = short.MinValue;
                        working = (short)workingF;
                        buffer[offset + (i * 2)] = (byte)(working & 0xff);
                        buffer[offset + (i * 2) + 1] = (byte)((working >> 8) & 0xff);
                    }

                    //Send events
                    this.clipped = this.clipped || clipped;
                    ClipEvent?.Invoke(this, clipped);
                }
            }

            //Copy to underlying
            underlyingStream.Write(buffer, offset, count);

            //Update size
            audioLength += count;
            UpdateLength();
        }

        public void WriteHeader()
        {
            //Calculate
            short blockAlign = CHANNELS * (BITS_PER_SAMPLE / 8);
            int avgBytesPerSec = sampleRate * (int)blockAlign;

            //Write
            WriteTag("RIFF");
            fileSizeOffs = this.underlyingStream.Position;
            WriteSignedInt(0);
            WriteTag("WAVE");
            WriteTag("fmt ");
            WriteSignedInt(16);
            WriteSignedShort(1); //Format tag
            WriteSignedShort(CHANNELS);
            WriteSignedInt(sampleRate);
            WriteSignedInt(avgBytesPerSec);
            WriteSignedShort(blockAlign);
            WriteSignedShort(BITS_PER_SAMPLE);
            WriteTag("data");
            dataSizeOffs = this.underlyingStream.Position;
            WriteSignedInt(0);
        }

        public void UpdateLength()
        {
            //Save current position
            long pos = this.underlyingStream.Position;

            //Update file length
            this.underlyingStream.Position = fileSizeOffs;
            WriteSignedInt(audioLength + 8);

            //Update data length
            this.underlyingStream.Position = dataSizeOffs;
            WriteSignedInt(audioLength);

            //Jump back
            this.underlyingStream.Position = pos;
        }

        private void WriteTag(string tag)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(tag);
            underlyingStream.Write(bytes, 0, bytes.Length);
        }

        private void WriteSignedInt(int value)
        {
            WriteEndianBytes(BitConverter.GetBytes(value));
        }

        private void WriteSignedShort(short value)
        {
            WriteEndianBytes(BitConverter.GetBytes(value));
        }

        private void WriteEndianBytes(byte[] data)
        {
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(data);
            underlyingStream.Write(data, 0, data.Length);
        }
    }
}
