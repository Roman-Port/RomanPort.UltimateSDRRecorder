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
        public AppSettingsDialog()
        {
            InitializeComponent();
        }

        public const string SOURCE_URL = "https://github.com/Roman-Port/RomanPort.UltimateSDRRecorder";

        private void openSourceBtn_Click(object sender, EventArgs e)
        {
            Process.Start(SOURCE_URL);
        }
    }
}
