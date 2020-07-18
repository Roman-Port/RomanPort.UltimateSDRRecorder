using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder.Framework.Ui
{
    public partial class RecorderControl : UserControl
    {
        public RecorderControl()
        {
            InitializeComponent();
        }

        public UltimateRecorder recorder;

        private void saveBufferBtn_Click(object sender, EventArgs e)
        {
            recorder.SaveBufferBtnPressed();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            recorder.StartRecordingBtnPressed();
        }

        private void saveBufferContinueBtn_Click(object sender, EventArgs e)
        {
            recorder.SaveBufferContinueBtnPressed();
        }

        public void SetRecorderTitle(string title)
        {
            recorderContainer.Text = title;
        }

        private void stopRecordingBtn_Click(object sender, EventArgs e)
        {
            recorder.StopBtnPressed();
        }

        private void saveRtAutoNameBtn_Click(object sender, EventArgs e)
        {
            recorder.StopBtnPressed(true);
        }



        public void SetInterfaceRecordingStatus(bool isRecording)
        {
            startBtn.Visible = !isRecording;
            saveBufferBtn.Visible = !isRecording;
            saveBufferContinueBtn.Visible = !isRecording;

            settingsBtn.Enabled = true;

            statusError.Text = "";
            statusError.Visible = isRecording;
            labelRecord.Visible = isRecording;
            infoRecord.Visible = isRecording;
            stopRecordingBtn.Visible = isRecording;
            saveRtAutoNameBtn.Visible = isRecording;
        }

        public void SetBufferStats(int seconds, float sizeMB)
        {
            infoBuffer.Text = $"{seconds.ToString()}s - {Math.Round(sizeMB, sizeMB > 999 ? 0 : 1)} MB";
        }

        public void SetRecordStats(TimeSpan time, float sizeMB)
        {
            infoRecord.Text = GenerateStatString(time, sizeMB);
        }

        public void SetAlertText(string text)
        {
            if(!savingState)
                statusError.Text = text;
        }

        private string GenerateStatString(TimeSpan time, float sizeMB)
        {
            return $"{time.Hours.ToString().PadLeft(2, '0')}:{time.Minutes.ToString().PadLeft(2, '0')}:{time.Seconds.ToString().PadLeft(2, '0')} - {Math.Round(sizeMB, sizeMB > 999 ? 0 : 1)} MB";
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            new RecorderSettingsPanel(recorder).ShowDialog();
        }

        public void SetSavingState()
        {
            SetSavingState(true);
            statusError.ForeColor = Color.White;
            statusError.Text = "SAVING...";
        }

        public void ClearSavingState()
        {
            SetSavingState(false);
            statusError.ForeColor = Color.FromArgb(255, 128, 128);
        }

        private bool savingState = false;

        private void SetSavingState(bool saving)
        {
            saveBufferBtn.Enabled = !saving;
            saveBufferContinueBtn.Enabled = !saving;
            settingsBtn.Enabled = !saving;
            startBtn.Enabled = !saving;
            stopRecordingBtn.Enabled = !saving;
            saveRtAutoNameBtn.Enabled = !saving;
            savingState = saving;
        }
    }
}
