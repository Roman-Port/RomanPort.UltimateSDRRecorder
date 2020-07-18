using RomanPort.UltimateSDRRecorder.Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder.Framework.Ui
{
    public partial class RdsAutoNameForm : Form
    {
        public RdsAutoNameForm(List<RdsAutoNameHistoryName> options, RdsAutoNameHistoryName defaultOption)
        {
            this.options = options;
            this.defaultOption = defaultOption;
            InitializeComponent();
        }

        private List<RdsAutoNameHistoryName> options;
        private RdsAutoNameHistoryName defaultOption;

        public string chosenName;

        private void saveBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            this.chosenName = defaultOption.text;
            Close();
        }

        private void manualFilenameSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.chosenName = null;
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void nameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveBtn.Enabled = nameList.SelectedItems.Count == 1;
        }

        private void RdsAutoNameForm_Load(object sender, EventArgs e)
        {
            nameList.Items.Clear();
            nameList.SuspendLayout();
            for(int i = options.Count - 1; i>=0; i--)
            {
                var p = options[i];
                if (p.text.Contains((char)0x00))
                    p.text = "X " + p.text;
                TimeSpan timeSinceFirstSeen = DateTime.UtcNow - p.seen;
                var row = new string[] { p.text, $"{(timeSinceFirstSeen.Days > 0 ? timeSinceFirstSeen.Days + " days, " : "")}{(timeSinceFirstSeen.Hours > 0 ? timeSinceFirstSeen.Hours + " hours, " : "")}{(timeSinceFirstSeen.Minutes > 0 ? timeSinceFirstSeen.Minutes + " minutes, " : "")}{timeSinceFirstSeen.Seconds} secconds ago" };
                var lv = new ListViewItem(row);
                lv.Tag = p;
                lv.Selected = p == defaultOption;
                nameList.Items.Add(lv);
            }
            nameList.ResumeLayout();
        }
    }
}
