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

        private AudioGainTester clippingTest;
        private bool hasClipped;

        public static readonly Color CLIPCOLOR_CURRENT = Color.FromArgb(255, 128, 128);
        public static readonly Color CLIPCOLOR_PAST = Color.FromArgb(255, 189, 128);

        private void RecorderSettingsPanel_Load(object sender, EventArgs e)
        {
            //Hide amplification part if we're recording baseband
            if (recorder.source.GetType() == typeof(BasebandSource))
            {
                ampGroup.Visible = false;
            } else
            {
                //Enable amplification clipping meter
                clippingTest = new AudioGainTester();
                clippingTest.ClippingChangedEvent += ClippingTest_ClippingChangedEvent;
                clippingTest.BeginTest(recorder.control);
                clippingMeter.BackColor = BackColor;
                clippingMeter.ForeColor = BackColor;
                UpdateAfAmp(recorder.settings.amplification);
            }

            //Hide clipping menu
            clippingMeter.BackColor = BackColor;
            clippingMeter.ForeColor = BackColor;

            //Set existing settings
            rewindBufferLength.Value = recorder.settings.rewind_buffer_length;
            SetRewindBufferLabel();
            afAmplicationTrack.Value = (int)(recorder.settings.amplification * 10f);
            afAmplificationLabel.Text = "AF Amplification: " + recorder.settings.amplification;
            rdsAutoNameOutput.Text = recorder.settings.rds_autoname_output_dir;
        }

        private void ClippingTest_ClippingChangedEvent(bool clipping)
        {
            hasClipped = hasClipped || clipping;
            Invoke((MethodInvoker)delegate
            {
                UpdateClippingMeter(clipping);
            });
        }

        private void UpdateClippingMeter(bool clipping)
        {
            if(clipping)
            {
                clippingMeter.BackColor = CLIPCOLOR_CURRENT;
                clippingMeter.ForeColor = Color.White;
            } else if (hasClipped)
            {
                clippingMeter.BackColor = CLIPCOLOR_PAST;
                clippingMeter.ForeColor = Color.White;
            } else
            {
                clippingMeter.BackColor = BackColor;
                clippingMeter.ForeColor = BackColor;
            }
        }

        private void SetRewindBufferLabel()
        {
            double sizeMB = Math.Round((float)recorder.GetSwapSizeFromSeconds((int)rewindBufferLength.Value) / 1024 / 1024, 1);
            rewindBufferLabel.Text = $"seconds / {sizeMB} MB";
        }

        private void RecorderSettingsPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Stop testing
            if(clippingTest != null)
            {
                clippingTest.EndTest();
            }
        }

        private void amplificationReset_Click(object sender, EventArgs e)
        {
            UpdateAfAmp(1);
        }

        private void afAmplicationTrack_Scroll(object sender, EventArgs e)
        {
            UpdateAfAmp((float)afAmplicationTrack.Value / 10);
        }

        private void UpdateAfAmp(float amp)
        {
            recorder.SetAmplification(amp);
            clippingTest.ChangeAmplification(amp);
            afAmplicationTrack.Value = (int)(amp * 10);
            afAmplificationLabel.Text = "AF Amplification: " + amp;
            hasClipped = false;
            UpdateClippingMeter(false);
        }

        private void applyBtn_Click(object sender, EventArgs e)
        {
            //Save RDS name
            recorder.settings.rds_autoname_output_dir = "";
            if(rdsAutoNameOutput.Text.Length > 0)
                recorder.settings.rds_autoname_output_dir = rdsAutoNameOutput.Text.TrimEnd('\\').TrimEnd('/') + "\\";

            //Save swap size
            bool swapChanged = rewindBufferLength.Value != recorder.settings.rewind_buffer_length;
            recorder.settings.rewind_buffer_length = (int)rewindBufferLength.Value;

            //Save config
            recorder.SaveSettings();

            //Apply rewind buffer size
            if(swapChanged)
                recorder.SetSwapLengthSeconds(recorder.settings.rewind_buffer_length);

            //Close
            Close();
        }

        private void rewindBufferLength_ValueChanged(object sender, EventArgs e)
        {
            //Update preview size
            SetRewindBufferLabel();
        }

        private void browseOutputFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "Choose the folder you'd like to save RDS auto-named recordings to.";
            var r = fd.ShowDialog();
            if (r == DialogResult.OK)
                rdsAutoNameOutput.Text = fd.SelectedPath;
        }
    }
}
