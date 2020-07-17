using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder.DVR.Interface.TriggerConfigDialogs
{
    public partial class RdsRadioTextTriggerConfig : ITriggerConfigDialog
    {
        public RdsRadioTextTriggerConfig()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        public const string TRIGGER_SETTING_TEXT = "TS_TEXT";
        public const string TRIGGER_SETTING_AUTOSTOP_ENABLED = "TS_AUTOSTOP_ENABLED";
        public const string TRIGGER_SETTING_AUTOSTOP_DELAY = "TS_AUTOSTOP_DELAY";

        private void saveBtn_Click(object sender, EventArgs e)
        {
            //Create settings
            trigger_settings = new List<string>();
            trigger_settings.Add(triggerTextBox.Text.ToUpper());
            trigger_settings.Add(autoStopEnabled.Checked.ToString());
            trigger_settings.Add(autoStopDelay.Value.ToString());
            DialogResult = DialogResult.OK;
        }

        private void autoStopEnabled_CheckedChanged(object sender, EventArgs e)
        {
            autoStopDelay.Enabled = autoStopEnabled.Checked;
        }
    }
}
