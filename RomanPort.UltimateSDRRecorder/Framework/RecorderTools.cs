using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder.Framework
{
    public static class RecorderTools
    {
        public static void NotifyUserNewThread(string title, string subtitle, MessageBoxIcon icon)
        {
            var t = new Thread(() =>
            {
                MessageBox.Show(subtitle, title, MessageBoxButtons.OK, icon);
            });
            t.IsBackground = true;
            t.Start();
        }

        public static float GetMegabytes(double value, int decimals)
        {
            return (float)Math.Round(value / 1024 / 1024, decimals);
        }


        public static bool AmplifyPCMSamples(byte[] buffer, int offset, int count, int bitsPerSample, float amplification)
        {
            //This function returns if it clipped
            bool clipped = false;

            //Calculate max values
            float maxValue = (float)short.MaxValue;
            float minValue = (float)short.MinValue;
            
            //Do amplification if needed
            if (amplification != 1)
            {
                //Amplify two bytes at a time
                if (bitsPerSample == 16)
                {
                    //Validate
                    if (count % 2 != 0)
                        throw new Exception("Cannot amplify, as there are an odd number of bytes.");

                    //Multiply
                    short working;
                    float workingF;
                    for (int i = 0; i < count / 2; i++)
                    {
                        workingF = (float)BitConverter.ToInt16(buffer, offset + (i * 2)) * amplification;
                        clipped = workingF > maxValue || workingF < minValue || clipped;
                        if (workingF > maxValue)
                            workingF = maxValue;
                        if (workingF < minValue)
                            workingF = minValue;
                        working = (short)workingF;
                        buffer[offset + (i * 2)] = (byte)(working & 0xff);
                        buffer[offset + (i * 2) + 1] = (byte)((working >> 8) & 0xff);
                    }
                }
            }

            return clipped;
        }
    }
}
