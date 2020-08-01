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

namespace RomanPort.UltimateSDRRecorder.Updater
{
    public partial class AppUpdatePrompt : Form
    {
        public AppUpdatePrompt(UltimateSDRRecorderPlugin plugin, AppUpdateData update)
        {
            InitializeComponent();
            this.plugin = plugin;
            this.update = update;
            updateText.Text = update.dialog_text.Replace("\\n", "\n");
        }

        public UltimateSDRRecorderPlugin plugin;
        public AppUpdateData update;

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (update.dialog_confirm_link.StartsWith("http"))
                Process.Start(update.dialog_confirm_link);
            Close();
        }

        private void ignoreBtn_Click(object sender, EventArgs e)
        {
            plugin.config.latest_skipped_version = update.latest_version;
            plugin.SaveConfig();
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
