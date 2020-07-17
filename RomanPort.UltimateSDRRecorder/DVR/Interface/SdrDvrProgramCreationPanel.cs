using RomanPort.UltimateSDRRecorder.DVR.Entities;
using RomanPort.UltimateSDRRecorder.DVR.Interface.TriggerConfigDialogs;
using RomanPort.UltimateSDRRecorder.Framework.Output;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder.DVR.Interface
{
    public partial class SdrDvrProgramCreationPanel : Form
    {
        public SdrDvrProgramCreationPanel()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            profile = new DvrProgramProfile
            {
                created_time = DateTime.UtcNow,
                program_title = settingName.Text,
                trigger_type = triggerRadioText.Checked ? SdrProgramProfileTrigger.RDSRadioTextTrigger : SdrProgramProfileTrigger.TimeTrigger,
                change_freq_enabled = changeFreqEnabled.Checked,
                change_freq_khz = (int)changeFreqDial.Value,
                record_iq = outputFormatRecordIq.Checked,
                output_path = outputPath.Text,
                trigger_args = null
            };
        }

        public DvrProgramProfile profile;

        private void saveBtn_Click(object sender, EventArgs e)
        {
            //Validate
            if(profile.trigger_args == null)
            {
                MessageBox.Show("You need to configure options for the trigger before you can save.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void settingName_TextChanged(object sender, EventArgs e)
        {
            profile.program_title = settingName.Text;
        }

        private void trigger_CheckedChanged(object sender, EventArgs e)
        {
            if (triggerRadioText.Checked)
                profile.trigger_type = SdrProgramProfileTrigger.RDSRadioTextTrigger;
            else if (triggerRadioText.Checked)
                profile.trigger_type = SdrProgramProfileTrigger.TimeTrigger;
            else
                MessageBox.Show("Invalid trigger type.");
            profile.trigger_args = null;
        }

        private void btnConfigureTrigger_Click(object sender, EventArgs e)
        {
            ITriggerConfigDialog dialog;

            //Create
            if (profile.trigger_type == SdrProgramProfileTrigger.RDSRadioTextTrigger)
                dialog = new RdsRadioTextTriggerConfig();
            else
            {
                MessageBox.Show("Invalid trigger type.");
                return;
            }

            //Show
            var r = dialog.ShowDialog();
            if(r == DialogResult.OK)
            {
                profile.trigger_args = dialog.trigger_settings;
            }
        }

        private void changeFreqEnabled_CheckedChanged(object sender, EventArgs e)
        {
            profile.change_freq_enabled = changeFreqEnabled.Checked;
        }

        private void changeFreqDial_ValueChanged(object sender, EventArgs e)
        {
            profile.change_freq_khz = (int)changeFreqDial.Value;
        }

        private void outputPath_TextChanged(object sender, EventArgs e)
        {
            profile.output_path = outputPath.Text;
        }

        private void outputBrowseBtn_Click(object sender, EventArgs e)
        {
            var path = IUltimateOutputTempFileEncoder.OpenFilePicker("wav");
            if(path != null)
            {
                outputPath.Text = path;
                profile.output_path = outputPath.Text;
            }
        }

        private void outputFormat_CheckedChanged(object sender, EventArgs e)
        {
            profile.record_iq = outputFormatRecordIq.Checked;
        }
    }
}
