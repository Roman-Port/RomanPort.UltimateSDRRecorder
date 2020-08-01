using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder
{
    public partial class AppSettingsDialog : Form
    {
        public AppSettingsDialog(UltimateSDRRecorderPlugin plugin)
        {
            InitializeComponent();
            this.plugin = plugin;

            this.updateCheckOn.Checked = plugin.config.check_updates;
            this.updateCheckOff.Checked = !plugin.config.check_updates;
        }

        private UltimateSDRRecorderPlugin plugin;

        public const string SOURCE_URL = "https://github.com/Roman-Port/RomanPort.UltimateSDRRecorder";

        private void openSourceBtn_Click(object sender, EventArgs e)
        {
            Process.Start(SOURCE_URL);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            plugin.config.check_updates = updateCheckOn.Checked;
            plugin.SaveConfig();
            Close();
        }
    }
}
