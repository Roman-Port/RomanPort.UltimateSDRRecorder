using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.Framework.Output
{
    /// <summary>
    /// Allows for encoding of files larger than 2 GB, the max for one wav file. Splits across multiple files
    /// </summary>
    public class WavMultipartFileEncoder : IUltimateOutputTempFileEncoder
    {
        public WavMultipartFileEncoder(int sampleRate) : base(sampleRate, "wav")
        {
            //Create encoder
            CreateNextEncoder();
        }

        public WavEncoder activeEncoder;

        public void CreateNextEncoder()
        {
            //Close old recorder, if any
            CloseActiveFile();

            //Create new encoder
            activeEncoder = new WavEncoder(RequestNextFile(), sampleRate);
        }

        public override void OnGetSamples(byte[] data)
        {
            //Check if we need to create a new file
            if (!activeEncoder.CanFitData(data.Length))
                CreateNextEncoder();

            //Write
            totalBytes += data.Length;
            activeEncoder.Write(data, 0, data.Length);
        }

        public override void CloseActiveFile()
        {
            if (activeEncoder != null)
            {
                activeEncoder.Flush();
                activeEncoder.Close();
            }
        }
    }
}
