using RomanPort.UltimateSDRRecorder.Framework.Output;
using RomanPort.UltimateSDRRecorder.Framework.Sources;
using RomanPort.UltimateSDRRecorder.Framework.Sources.Processors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder.Framework.Ui
{
    public partial class RecorderSettingsPanel : Form
    {
        public RecorderSettingsPanel(UltimateRecorder recorder)
        {
            InitializeComponent();
            this.recorder = recorder;
        }

        public UltimateRecorder recorder;

        //USE WITH CAUTION. This may throw an exception if not used carefully
        public WavEncoder wavEncoder { get { return null; } }

        public static readonly Color CLIPCOLOR_CURRENT = Color.FromArgb(255, 128, 128);
        public static readonly Color CLIPCOLOR_PAST = Color.FromArgb(255, 189, 128);

        private bool clipping;
        private System.Timers.Timer clippingMeterTimer;

        private void afAmplicationTrack_Scroll(object sender, EventArgs e)
        {
            float amp = ((float)afAmplicationTrack.Value) / 10f;
            afAmplificationLabel.Text = "AF Amplification: " + amp;

            //Set
            recorder.SetAmplification(amp);
        }

        private void RecorderSettingsPanel_Load(object sender, EventArgs e)
        {
            //Only if we're recording do we run the clipping meter
            if (recorder.activeEncoder != null)
            {
                //Subscribe to the clipping events so we can show them
                wavEncoder.ClipEvent += WavEncoder_ClipEvent;

                //Start clipping meter timer
                clippingMeterTimer = new System.Timers.Timer(50);
                clippingMeterTimer.Elapsed += ClippingMeterTimer_Elapsed;
                clippingMeterTimer.AutoReset = true;
                clippingMeterTimer.Start();
            }

            //Hide amplification part if we're recording baseband
            if (recorder.source.GetType() == typeof(BasebandSource))
                ampGroup.Visible = false;

            //Hide clipping menu
            clippingMeter.BackColor = BackColor;
            clippingMeter.ForeColor = BackColor;

            //Set existing settings
            rewindBufferLength.Value = recorder.settings.rewind_buffer_length;
            SetRewindBufferLabel();
            afAmplicationTrack.Value = (int)(recorder.settings.amplification * 10f);
            afAmplificationLabel.Text = "AF Amplification: " + recorder.settings.amplification;
        }

        private void SetRewindBufferLabel()
        {
            double sizeMB = Math.Round((float)recorder.GetSwapSizeFromSeconds((int)rewindBufferLength.Value) / 1024 / 1024, 1);
            rewindBufferLabel.Text = $"seconds / {sizeMB} MB";
        }

        private void ClippingMeterTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                //Calculate
                Color c;
                Color f;
                if (clipping)
                {
                    c = CLIPCOLOR_CURRENT;
                    f = Color.White;
                }
                else if (wavEncoder.clipped)
                {
                    c = CLIPCOLOR_PAST;
                    f = Color.White;
                }
                else
                {
                    c = BackColor;
                    f = BackColor;
                }

                //Set
                clippingMeter.BackColor = c;
                clippingMeter.ForeColor = f;
            }
            catch
            {
                //Ignore
            }
        }

        private void WavEncoder_ClipEvent(WavEncoder wav, bool clipping)
        {
            this.clipping = clipping;
        }

        private void RecorderSettingsPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clippingMeterTimer != null)
            {
                //Remove events
                wavEncoder.ClipEvent -= WavEncoder_ClipEvent;

                //Kill timer
                clippingMeterTimer.Stop();
            }
        }

        private void amplificationReset_Click(object sender, EventArgs e)
        {
            recorder.SetAmplification(1);
            afAmplicationTrack.Value = 10;
            afAmplificationLabel.Text = "AF Amplification: 1";
        }

        private void applyBtn_Click(object sender, EventArgs e)
        {
            //Save settings (amplification has already been updated)

            //Save swap length only if it has changed
            if(recorder.settings.rewind_buffer_length != (int)rewindBufferLength.Value)
                recorder.SetSwapLengthSeconds((int)rewindBufferLength.Value);

            //Close
            Close();
        }

        private void rewindBufferLength_ValueChanged(object sender, EventArgs e)
        {
            //Update preview size
            SetRewindBufferLabel();
        }
    }
}
